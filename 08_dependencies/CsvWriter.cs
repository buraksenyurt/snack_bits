using Sdk;

namespace AzonWorks;

public class CsvWriter
    : IFileWriter<Game>
{
    public Result Write(string targetFile, List<Game> source)
    {
        return new Result { Status = Status.FileSaved, Message = "CSV formatında kaydedildi" };
    }
}