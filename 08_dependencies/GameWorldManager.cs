namespace AzonWorks;

public class GameWorldManager
{
    private readonly IList<Game> games = new List<Game>();

    public IList<Game> Games => games;

    public Result Add(Game game)
    {
        if (!games.Contains(game))
        {
            games.Add(game);
            return new Result { Status = Status.Added, Title = "Oyun eklendi." };
        }
        return new Result { Status = Status.AlreadyExist, Title = "Oyun zaten listede yer alıyor." };
    }
}