namespace BenchmarkApp;

public class DataRepository
{
    private readonly List<Game> games;
    public DataRepository()
    {
        games =
        [
            new Game { Id = 1, Title = "Super Mario Bros.", Genre = "Platform", ReleaseDate = new DateTime(1985, 9, 13), Developer = "Nintendo", InStock = true, Price = 59.99m, AverageUserPoint = 9.5 },
            new Game { Id = 2, Title = "The Legend of Zelda", Genre = "Action-Adventure", ReleaseDate = new DateTime(1986, 2, 21), Developer = "Nintendo", InStock = true, Price = 49.99m, AverageUserPoint = 9.2 },
            new Game { Id = 3, Title = "Tetris", Genre = "Puzzle", ReleaseDate = new DateTime(1984, 6, 6), Developer = "Alexey Pajitnov", InStock = true, Price = 29.99m, AverageUserPoint = 9.0 },
            new Game { Id = 4, Title = "Metroid", Genre = "Action", ReleaseDate = new DateTime(1986, 8, 6), Developer = "Nintendo", InStock = false, Price = 44.99m, AverageUserPoint = 8.8 },
            new Game { Id = 5, Title = "Mega Man 2", Genre = "Platform", ReleaseDate = new DateTime(1988, 12, 24), Developer = "Capcom", InStock = true, Price = 34.99m, AverageUserPoint = 8.7 },
            new Game { Id = 6, Title = "Castlevania", Genre = "Action", ReleaseDate = new DateTime(1986, 9, 26), Developer = "Konami", InStock = true, Price = 39.99m, AverageUserPoint = 8.5 },
            new Game { Id = 7, Title = "Final Fantasy", Genre = "RPG", ReleaseDate = new DateTime(1987, 12, 18), Developer = "Square", InStock = true, Price = 59.99m, AverageUserPoint = 9.1 },
            new Game { Id = 8, Title = "Duck Hunt", Genre = "Shooter", ReleaseDate = new DateTime(1984, 4, 21), Developer = "Nintendo", InStock = false, Price = 19.99m, AverageUserPoint = 7.5 },
            new Game { Id = 9, Title = "Punch-Out!!", Genre = "Sports", ReleaseDate = new DateTime(1987, 9, 18), Developer = "Nintendo", InStock = true, Price = 49.99m, AverageUserPoint = 8.6 },
            new Game { Id = 10, Title = "Excitebike", Genre = "Racing", ReleaseDate = new DateTime(1984, 11, 30), Developer = "Nintendo", InStock = true, Price = 24.99m, AverageUserPoint = 7.8 }
        ];
    }
    public List<Game> GetGames() => games;
}
