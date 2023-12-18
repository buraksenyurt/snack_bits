using AzonWorks;

namespace _08_dependencies;

class Program
{
    static void Main()
    {
        var steamly = new GameWorldManager();
        steamly.Add(new Game(1, "Command & Conquer Generals Zero Hour", 7.3));
        steamly.Add(new Game(2, "Senssible Soccer", 5.8));
    }
}
