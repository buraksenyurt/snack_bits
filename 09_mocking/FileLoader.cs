
namespace KanbanWorld;

public class FileLoader
    : IFileLoader
{
    public IEnumerable<string> LoadLines(string filePath)
    {
        return File.ReadLines(filePath);
    }
}