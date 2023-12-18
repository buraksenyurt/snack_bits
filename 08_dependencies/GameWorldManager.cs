using Sdk;

namespace AzonWorks;

public class GameWorldManager
{
    private readonly IList<Game> games = new List<Game>();

    public IList<Game> Games => games;

    public Result AddGame(Game game)
    {
        if (!games.Contains(game))
        {
            games.Add(game);
            return new Result { Status = Status.Added, Message = "Oyun eklendi." };
        }
        return new Result { Status = Status.AlreadyExist, Message = "Oyun zaten listede yer alıyor." };
    }
}