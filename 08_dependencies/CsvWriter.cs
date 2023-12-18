using System.Text;
using Sdk;

namespace AzonWorks;

public class CsvWriter
    : IFileWriter<Game>
{
    public Result Write(string fileName, List<Game> source)
    {
        try
        {
            var targetPath = Path.Combine(Environment.CurrentDirectory, $"{fileName}.csv");
            Console.WriteLine(targetPath);
            StringBuilder builder = new();
            foreach (var game in source)
            {
                builder.AppendLine(game.ToString());
            }
            File.WriteAllText(targetPath, builder.ToString());
        }
        catch (DirectoryNotFoundException excp)
        {
            return new Result { Status = Status.TargetFileError, Message = excp.Message };
        }
        catch (Exception excp)
        {
            return new Result { Status = Status.FileSaveError, Message = excp.Message };
        }
        return new Result { Status = Status.FileSaved, Message = "CSV formatında kaydedildi" };
    }
}