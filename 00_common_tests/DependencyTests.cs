using AzonWorks;
using Sdk;

namespace _00_common_tests;

public class DependencyTests
{
    [Fact]
    public void Add_New_Game_To_GamersWorld_List_Test()
    {
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        var actual = gameWorldManager.AddGame(aGame).Status;
        var expected = new Result { Status = Status.Added, Message = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Game_List_Already_Contains_Incoming_Game_Test()
    {
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        var _ = gameWorldManager.AddGame(aGame);
        var actual = gameWorldManager.AddGame(aGame).Status;
        var expected = new Result { Status = Status.AlreadyExist, Message = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Added_A_Few_Games_Returns_A_Filled_Game_List()
    {
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        gameWorldManager.AddGame(aGame);
        gameWorldManager.AddGame(new(2, "Flashback", 7.6));
        gameWorldManager.AddGame(new(3, "Prince of Persia", 5.6));
        gameWorldManager.AddGame(new(4, "Super Mario", 9.5));
        var actual = gameWorldManager.Games.Count();
        var expected = 4;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SaveAll_Is_Invalid_For_Invalid_Path()
    {
        var fileName = "xyz://games";
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        gameWorldManager.AddGame(aGame);
        gameWorldManager.AddGame(new(2, "Flashback", 7.6));
        gameWorldManager.AddGame(new(3, "Prince of Persia", 5.6));
        gameWorldManager.AddGame(new(4, "Super Mario", 9.5));
        var actual = gameWorldManager.SaveAll(fileName, new CsvWriter()).Status;
        var expected = new Result { Status = Status.TargetFileError, Message = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SaveAll_Is_Ok_For_Valid_Path_With_Csv_Format()
    {
        var fileName = "games";
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        gameWorldManager.AddGame(aGame);
        gameWorldManager.AddGame(new(2, "Flashback", 7.6));
        gameWorldManager.AddGame(new(3, "Prince of Persia", 5.6));
        gameWorldManager.AddGame(new(4, "Super Mario", 9.5));
        var actual = gameWorldManager.SaveAll(fileName, new CsvWriter()).Status;
        var expected = new Result { Status = Status.FileSaved, Message = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SaveAll_Is_Ok_For_Valid_Path_With_Json_Format()
    {
        var fileName = "games";
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        gameWorldManager.AddGame(aGame);
        gameWorldManager.AddGame(new(2, "Flashback", 7.6));
        gameWorldManager.AddGame(new(3, "Prince of Persia", 5.6));
        gameWorldManager.AddGame(new(4, "Super Mario", 9.5));
        var actual = gameWorldManager.SaveAll(fileName, new JSonWriter()).Status;
        var expected = new Result { Status = Status.FileSaved, Message = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }
}