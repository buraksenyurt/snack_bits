use crate::case::Case;
use crate::constants::*;
use std::path::{Path, PathBuf};
use std::{fs, thread};

#[derive(Copy, Clone)]
pub enum Mode {
    Single,
    Multi,
}

pub struct Shearlock;

impl Shearlock {
    pub fn run(project_folder: &PathBuf, mode: Mode) {
        let files = Shearlock::get_files(project_folder);
        let handles: Vec<_> = files
            .into_iter()
            .map(|file| {
                thread::spawn(move || {
                    let file_name = &file.file_name().unwrap().to_str().unwrap();
                    //println!("{} işlenecek", &file.file_name().unwrap().to_string_lossy());
                    match fs::read_to_string(&file) {
                        Ok(content) => {
                            Shearlock::process_file(file_name, &content, mode);
                        }
                        Err(e) => {
                            println!("{}", e);
                        }
                    };
                })
            })
            .collect();

        for handle in handles {
            handle.join().unwrap();
        }
    }

    fn process_file(file_name: &str, content: &str, mode: Mode) {
        let blocks = Self::scan_switch_case(content);
        match mode {
            Mode::Single => {
                let mut code = String::new();
                let mut namespace = String::new();
                let mut namespace_read: bool = false;
                for mut block in blocks {
                    if !namespace_read {
                        namespace = block.namespace;
                        namespace_read = true;
                    }
                    block.name.retain(|c| c != '.');
                    code.push_str(
                        format!(
                            r"
                            public class {} : IBusiness
                            {{
                                public void Apply() {{
                                    {}
                                }}
                            }}",
                            block.name, block.content
                        )
                        .as_str(),
                    );
                }
                let mut last_code = format!("namespace {}", namespace);
                last_code.push_str(code.as_str());
                if !code.is_empty() {
                    fs::write(format!("./output/{}.cs", file_name), last_code).unwrap();
                }
            }
            Mode::Multi => {
                for mut block in blocks {
                    block.name.retain(|c| c != '.');
                    let content = format!(
                        r"namespace {}
                        public class {} : IBusiness
                        {{
                            public void Apply() {{
                                {}
                            }}
                        }}",
                        block.namespace, block.name, block.content
                    );
                    fs::write(format!("./output/{}.cs", block.name), content).unwrap();
                }
            }
        }
    }

    pub fn get_files(dir: &PathBuf) -> Vec<PathBuf> {
        let mut files = Vec::new();
        let entries = fs::read_dir(dir).unwrap();

        for entry in entries.flatten() {
            let path = entry.path();
            if path.is_file()
                && path.extension().map_or(false, |ext| ext == "cs")
                && Self::check_file_name(&path)
            {
                files.push(path.clone());
            }
            if path.is_dir() {
                let sub_files = Self::get_files(&path);
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

    pub fn scan_switch_case(content: &str) -> Vec<Case> {
        let mut result = Vec::new();

        let mut in_switch = false;
        let mut in_case = false;
        let mut case_line = String::new();
        let mut case_block = String::new();
        let mut namespace_founded = false;
        let mut namespace_name: Box<String> = Box::default();

        for line in content.lines() {
            if !namespace_founded && line.contains(NAMESPACE_EXPRESSION) {
                namespace_founded = true;
                namespace_name = Box::from(
                    line.trim_start_matches(NAMESPACE_EXPRESSION)
                        .trim()
                        .to_string(),
                );
                continue;
            }
            if line.contains(SWITCH_EXPRESSION) {
                in_switch = true;
                continue;
            }

            if in_switch {
                if line.contains(CASE_EXPRESSION) {
                    in_case = true;

                    if !case_line.is_empty() {
                        result.push(Case {
                            namespace: namespace_name.to_string(),
                            name: case_line
                                .trim_start_matches(CASE_EXPRESSION)
                                .trim()
                                .to_string(),
                            content: case_block.clone(),
                        });
                        case_block.clear();
                    }

                    let case_line_parts: Vec<&str> =
                        line.trim().splitn(2, COLON_EXPRESSION).collect();
                    if let Some(name) = case_line_parts.first() {
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
                namespace: namespace_name.to_string(),
                name: case_line
                    .trim_start_matches(CASE_EXPRESSION)
                    .trim()
                    .to_string(),
                content: case_block.clone(),
            });
        }

        result
    }
}
