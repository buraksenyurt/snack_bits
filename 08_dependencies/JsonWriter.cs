using System.Text.Json;
using Sdk;

namespace AzonWorks;

public class JSonWriter
    : IWriter<Game>
{
    public Result Write(string fileName, List<Game> source)
    {
        try
        {
            var targetPath = Path.Combine(Environment.CurrentDirectory, $"{fileName}.json");
            var content = JsonSerializer.Serialize(source, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(targetPath, content);
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