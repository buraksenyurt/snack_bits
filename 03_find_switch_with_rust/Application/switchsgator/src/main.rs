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
                // thread::spawn(move || {
                    println!("{} işlenecek", &file.file_name().unwrap().to_string_lossy());
                    match fs::read_to_string(file) {
                        Ok(content) => {
                            let blocks = scan_switch_case(&content, false);
                            for block in blocks {
                                println!("\n{}\n{}", block.name,block.content);
                            }
                        }
                        Err(e) => {
                            println!("{}", e);
                            return;
                        }
                    };
                // })
            })
            .collect();

        // for handle in handles {
        //     handle.join().unwrap();
        // }
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

fn scan_switch_case(content: &str, use_default: bool) -> Vec<Case> {
    let mut blocks = Vec::new();
    let mut in_switch = false;
    let mut in_default = false;
    let mut expression = String::new();
    let mut current_statement = String::new();
    let mut case: Case = Case {
        name: "".to_string(),
        content: "".to_string(),
    };

    for line in content.lines() {
        if line.contains("switch") {
            in_switch = true;
        } else if in_switch && !in_default {
            if let Some(case_start) = line.find("case") {
                let case_line = &line[case_start + 4..];
                let trimmed_line = case_line.trim_start();

                if let Some(end_index) = trimmed_line.find(':') {
                    expression.push_str(&trimmed_line[..end_index]);
                    case.name = expression.clone();
                    //blocks.push(expression.clone());
                    expression.clear();
                }
            } else if line.contains("break;") {
                if !current_statement.is_empty() {
                    case.content = current_statement.trim().to_string();
                    blocks.push(case.clone());
                    current_statement.clear();
                }
            } else if !line.trim().is_empty() {
                current_statement.push_str(line);
                current_statement.push('\n');
            }
        }

        if line.contains("default:") {
            in_default = true;
            if use_default {
                case.name = "default".to_string();
                //blocks.push("default".to_string());
            }
        }

        if in_default && line.contains(':') {
            in_default = false;
        }
    }

    if !current_statement.is_empty() {
        case.content = current_statement.trim().to_string();
        blocks.push(case);
    }

    blocks
}

#[derive(Clone)]
pub struct Case {
    pub name: String,
    pub content: String,
}

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
        let blocks = scan_switch_case(code_block, false);
        assert_eq!(blocks.len(), 3);

        let blocks = scan_switch_case(code_block, true);
        assert_eq!(blocks.len(), 4);
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
