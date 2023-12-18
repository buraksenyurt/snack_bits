using AzonWorks;

namespace _08_dependencies;

class Program
{
    static void Main()
    {
        var steamly = new GameWorldManager();
        steamly.AddGame(new Game(1, "Command & Conquer Generals Zero Hour", 7.3));
        steamly.AddGame(new Game(2, "Senssible Soccer", 5.8));
        steamly.AddGame(new Game(3, "Flashback", 7.4));
        steamly.AddGame(new Game(4, "Super Mario Boss", 9.1));

        steamly.SaveAll("PopularGames", new CsvWriter());
        steamly.SaveAll("PopularGames", new JSonWriter());
    }
}
