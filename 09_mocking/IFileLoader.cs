namespace KanbanWorld;

public interface IFileLoader
{
    IEnumerable<string> LoadLines(string filePath);
}