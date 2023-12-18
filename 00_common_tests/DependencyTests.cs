using AzonWorks;

namespace _00_common_tests;

public class DependencyTests
{
    [Fact]
    public void Add_New_Game_To_GamersWorld_List_Test()
    {
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        var actual = gameWorldManager.Add(aGame).Status;
        var expected = new Result { Status = Status.Added, Title = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Game_List_Already_Contains_Incoming_Game_Test()
    {
        GameWorldManager gameWorldManager = new();
        Game aGame = new(1, "World of Warcraft", 8.4);
        var _ = gameWorldManager.Add(aGame);
        var actual = gameWorldManager.Add(aGame).Status;
        var expected = new Result { Status = Status.AlreadyExist, Title = string.Empty }.Status;
        Assert.Equal(expected, actual);
    }
}