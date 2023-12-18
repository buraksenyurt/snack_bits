using Sdk;

namespace AzonWorks;

public class GameWorldManager
{
    private readonly IList<Game> games = new List<Game>();
    //private readonly IWriter<Game> _writer;
    public IList<Game> Games => games;
    // public GameWorldManager(IWriter<Game> writer)
    // {
    //     _writer = writer;
    // }

    public Result AddGame(Game game)
    {
        if (!games.Contains(game))
        {
            games.Add(game);
            return new Result { Status = Status.Added, Message = "Oyun eklendi." };
        }
        return new Result { Status = Status.AlreadyExist, Message = "Oyun zaten listede yer alıyor." };
    }

    public Result SaveAll(string fileName, IWriter<Game> writer)
    {
        var result = writer.Write(fileName, (List<Game>)games);
        return result;
    }
}