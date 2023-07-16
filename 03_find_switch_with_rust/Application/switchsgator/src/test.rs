#[cfg(test)]
mod test {
    use crate::Shearlock;
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
        let blocks = Shearlock::scan_switch_case(code_block);
        assert_eq!(blocks.len(), 3);
    }
    #[test]
    fn should_get_files_works_test() {
        let project_folder = PathBuf::from(r"../BusinessLib");
        let files = Shearlock::get_files(&project_folder);
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
