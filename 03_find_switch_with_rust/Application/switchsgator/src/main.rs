mod case;
mod constants;
mod shearlock;
mod test;

use crate::shearlock::Shearlock;
use clap::{Arg,Command};
use std::path::PathBuf;

fn main() {
    let arguments = Command::new("Switch Conquer")
        .version("1.0")
        .author("BSŞ")
        .about("Cognitive Complexity değerlerini azaltmak için switch bloklarını sınıflara ayırır")
        .arg(
            Arg::new("mode")
                .required(true)
                .value_parser(["single", "multi"])
                .help("Çıktı tek dosyaya 'single' birden fazla dosyaya mı 'multi' olacak"),
        )
        .arg(
            Arg::new("source")
                .required(true)
                .help("Tarama yapılacak olan klasör bilgisi"),
        )
        .get_matches();

    match arguments
        .get_one::<String>("mode")
        .expect("Bir çıktı modu belirtilmeli")
        .as_str()
    {
        "single" => {
            println!("Çıktılar tek dosyaya toplanacak")
        }
        "multi" => {
            println!("Çıktılar birden fazla sınıf dosyasına ayrılacak");
            let folder = arguments.get_one::<String>("source").unwrap();
            let project_folder = PathBuf::from(folder);
            if project_folder.exists() {
                Shearlock::run(&project_folder)
            } else {
                println!("'{}'. Bu path geçerli değil.", folder);
            }
        }
        _ => {
            println!("Ters giden bir şeyler var")
        }
    }
}
