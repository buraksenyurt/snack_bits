using Sdk;

namespace AzonWorks;

public class GameWorldManager
{
    private readonly IList<Game> games = new List<Game>();
    private readonly IFileWriter<Game> _fileWriter;
    public IList<Game> Games => games;
    public GameWorldManager(IFileWriter<Game> fileWriter)
    {
        _fileWriter = fileWriter;
    }

    public Result AddGame(Game game)
    {
        if (!games.Contains(game))
        {
            games.Add(game);
            return new Result { Status = Status.Added, Message = "Oyun eklendi." };
        }
        return new Result { Status = Status.AlreadyExist, Message = "Oyun zaten listede yer alıyor." };
    }

    public Result SaveAll(string fileName)
    {       
        var result = _fileWriter.Write(fileName, (List<Game>)games);
        return result;
    }
}