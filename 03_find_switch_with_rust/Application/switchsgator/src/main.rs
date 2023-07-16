mod test;
mod shearlock;
mod constants;
mod case;

use std::env;
use std::path::PathBuf;
use crate::shearlock::Shearlock;

fn main() {
    let args: Vec<String> = env::args().collect();
    if args.len() < 2 {
        println!("Taramanın yapılacağı klasör bilgisini vermelisiniz.");
        return;
    }

    let project_folder = PathBuf::from(&args[1]);
    if project_folder.exists() {
        Shearlock::run(&project_folder)
    } else {
        println!("'{}'. Bu path geçerli değil.", &args[1]);
    }
}
