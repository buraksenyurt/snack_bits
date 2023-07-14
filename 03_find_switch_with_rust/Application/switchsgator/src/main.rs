use std::path::{Path, PathBuf};
use std::{env, fs};

fn main() {
    let args: Vec<String> = env::args().collect();
    if args.len() < 2 {
        println!("Taramanın yapılacağı klasör path bilgisini vermelisiniz.");
        return;
    }

    let project_folder = PathBuf::from(&args[1]);
    if project_folder.exists() {
        let files = get_files(&project_folder);
        for f in files {
            println!("{}", f.file_name().unwrap().to_string_lossy());
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
    if path
        .file_name()
        .unwrap()
        .to_string_lossy()
        .contains("AssemblyInfo")
        || path
            .file_name()
            .unwrap()
            .to_string_lossy()
            .contains("AssemblyAttributes")
        || path
            .file_name()
            .unwrap()
            .to_string_lossy()
            .contains("GlobalUsings")
    {
        return false;
    }
    true
}

#[cfg(test)]
mod test {
    use crate::get_files;
    use std::path::PathBuf;

    #[test]
    fn should_get_files_works_test() {
        let project_folder = PathBuf::from(r"../BusinessLib");
        let files = get_files(&project_folder);
        assert_eq!(files.len(), 2);
        assert_eq!(files[0].file_name().unwrap(), "SalesWorks.cs");
        // assert_eq!(
        //     files[1].file_name().unwrap(),
        //     ".NETCoreApp,Version=v7.0.AssemblyAttributes.cs"
        // );
        // assert_eq!(files[2].file_name().unwrap(), "BusinessLib.AssemblyInfo.cs");
        // assert_eq!(
        //     files[3].file_name().unwrap(),
        //     "BusinessLib.GlobalUsings.g.cs"
        // );
        assert_eq!(files[1].file_name().unwrap(), "OrderWorks.cs");
    }
}
