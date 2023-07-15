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
                    println!("{} işlenecek", &file.file_name().unwrap().to_string_lossy());
                    match fs::read_to_string(file) {
                        Ok(content) => {
                            let blocks = scan_switch_case(&content);
                            for block in blocks {
                                println!("{}\n{}", block.name, block.content);
                            }
                        }
                        Err(e) => {
                            println!("{}", e);
                            return;
                        }
                    };
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

fn scan_switch_case(content: &str) -> Vec<Case> {
    let mut result = Vec::new();

    let mut in_switch = false;
    let mut in_case = false;
    let mut case_line = String::new();
    let mut case_block = String::new();

    for line in content.lines() {
        if line.contains(SWITCH_EXPRESSION) {
            in_switch = true;
            continue;
        }

        if in_switch {
            if line.contains(CASE_EXPRESSION) {
                in_case = true;

                if !case_line.is_empty() {
                    result.push(Case {
                        name: case_line
                            .trim_start_matches(CASE_EXPRESSION)
                            .trim()
                            .to_string(),
                        content: case_block.clone(),
                    });
                    case_block.clear();
                }

                let case_line_parts: Vec<&str> = line.trim().splitn(2, COLON_EXPRESSION).collect();
                if let Some(name) = case_line_parts.get(0) {
                    case_line = name.to_string();
                }
            } else if in_case {
                if line.trim() == BREAK_EXPRESSION {
                    in_case = false;
                } else {
                    case_block.push_str(line);
                    case_block.push('\n');
                }
            }
        }
    }

    if !case_line.is_empty() {
        result.push(Case {
            name: case_line
                .trim_start_matches(CASE_EXPRESSION)
                .trim()
                .to_string(),
            content: case_block.clone(),
        });
    }

    result
}

#[derive(Clone)]
pub struct Case {
    pub name: String,
    pub content: String,
}

pub const SWITCH_EXPRESSION: &str = "switch";
pub const CASE_EXPRESSION: &str = "case";
pub const BREAK_EXPRESSION: &str = "break;";
pub const COLON_EXPRESSION: &str = ":";

#[cfg(test)]
mod test {
    use crate::{get_files, scan_switch_case};
    use std::path::PathBuf;

    #[test]
    fn should_scan_switch_case_works_test() {
        let code_block = r#"
            switch (customerType)
            {
                case CustomerType.TypeA:
                    Console.WriteLine("Case for Type A");
                    break;
                case CustomerType.TypeB:
                    Console.WriteLine("Case for Type B");
                    break;
                case CustomerType.TypeC:
                    Console.WriteLine("Case for Type C");
                    break;
                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        "#;
        let blocks = scan_switch_case(code_block);
        assert_eq!(blocks.len(), 3);
    }
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
                .filter(|&name| {
                    name.file_name().unwrap().to_string_lossy() == "TargetRegion.cs"
                })
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
                .filter(|&name| {
                    name.file_name().unwrap().to_string_lossy() == "CustomerType.cs"
                })
                .count(),
            1
        );
    }
}
