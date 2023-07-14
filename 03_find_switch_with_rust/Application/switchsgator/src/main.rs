use std::path::{Path, PathBuf};
use std::{env, fs, thread};

fn main() {
    let args: Vec<String> = env::args().collect();
    if args.len() < 2 {
        println!("Taramanın yapılacağı klasör path bilgisini vermelisiniz.");
        return;
    }

    let project_folder = PathBuf::from(&args[1]);
    if project_folder.exists() {
        let files = get_files(&project_folder);
        let handles: Vec<_> = files
            .into_iter()
            .map(|file| {
                thread::spawn(move || {
                    println!("{} işlenecek", &file.file_name().unwrap().to_string_lossy())
                })
            })
            .collect();

        for handle in handles {
            handle.join().unwrap();
        }
    } else {
        println!("'{}'. Bu path geçerli değil.", &args[1]);
    }
}

fn get_files(dir: &PathBuf) -> Vec<PathBuf> {
    let mut files = Vec::new();
    let entries = fs::read_dir(dir).unwrap();

    for entry in entries.flatten() {
        let path = entry.path();
        if path.is_file()
            && path.extension().map_or(false, |ext| ext == "cs")
            && check_file_name(&path)
        {
            files.push(path.clone());
        }
        if path.is_dir() {
            let sub_files = get_files(&path);
            files.extend(sub_files);
        }
    }

    files
}

fn check_file_name(path: &Path) -> bool {
    let dont_look_files = vec!["AssemblyInfo", "AssemblyAttributes", "GlobalUsings"];
    !dont_look_files
        .iter()
        .any(|&name| path.file_name().unwrap().to_string_lossy().contains(name))
}

#[cfg(test)]
mod test {
    use crate::get_files;
    use std::path::PathBuf;

    #[test]
    fn should_get_files_works_test() {
        let project_folder = PathBuf::from(r"../BusinessLib");
        let files = get_files(&project_folder);
        assert_eq!(files.len(), 6);
        assert_eq!(
            files
                .iter()
                .filter(|&name| { name.file_name().unwrap().to_string_lossy() == "OrderWorks.cs" })
                .count(),
            1
        );
        assert_eq!(
            files
                .iter()
                .filter(|&name| { name.file_name().unwrap().to_string_lossy() == "SalesWorks.cs" })
                .count(),
            1
        );
        assert_eq!(
            files
                .iter()
                .filter(|&name| { name.file_name().unwrap().to_string_lossy() == "Customer.cs" })
                .count(),
            1
        );
        assert_eq!(
            files
                .iter()
                .filter(|&name| { name.file_name().unwrap().to_string_lossy() == "TargetRegion.cs" })
                .count(),
            1
        );
        assert_eq!(
            files
                .iter()
                .filter(|&name| { name.file_name().unwrap().to_string_lossy() == "Weight.cs" })
                .count(),
            1
        );
        assert_eq!(
            files
                .iter()
                .filter(|&name| { name.file_name().unwrap().to_string_lossy() == "CustomerType.cs" })
                .count(),
            1
        );
    }
}
