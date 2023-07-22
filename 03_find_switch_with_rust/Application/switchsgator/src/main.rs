mod case;
mod constants;
mod shearlock;
mod test;

use crate::shearlock::{Mode, Shearlock};
use clap::{Arg, ArgMatches, Command};
use std::path::PathBuf;

fn main() {
    let arguments = Command::new("Switch Conquer")
        .version("1.0")
        .author("BSŞ")
        .about("Cognitive Complexity değerlerini azaltmak için switch bloklarını sınıflara ayırır")
        .arg(
            Arg::new("mode")
                .short('m')
                .long("mode")
                .required(true)
                .value_parser(["single", "multi"])
                .help("Çıktılar tek dosyaya mı yoksa ayrı ayrı birden fazla dosyaya mı alınacak?"),
        )
        .arg(
            Arg::new("source")
                .short('s')
                .long("source")
                .required(true)
                .help("Tarama yapılacak olan kaynak klasör bilgisidir."),
        )
        .get_matches();

    match arguments
        .get_one::<String>("mode")
        .expect("Bir çıktı modu belirtilmeli.")
        .as_str()
    {
        "single" => {
            println!("Çıktılar tek dosyaya toplanacak");
            run(arguments, Mode::Single);
        }
        "multi" => {
            println!("Çıktılar birden fazla sınıf dosyasına ayrılacak");
            run(arguments, Mode::Multi);
        }
        _ => {
            println!("Ters giden bir şeyler var")
        }
    }
}

fn run(arguments: ArgMatches, mode: Mode) {
    let folder = arguments.get_one::<String>("source").unwrap();
    let project_folder = PathBuf::from(folder);
    if project_folder.exists() {
        Shearlock::run(&project_folder, mode)
    } else {
        println!("'{}'. Bu path geçerli değil.", folder);
    }
}
