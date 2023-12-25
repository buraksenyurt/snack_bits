namespace KanbanWorld;
public class Manager
{
    private readonly List<WorkItem> _workItems;
    private readonly IFileLoader _fileLoader;

    public Manager(IFileLoader fileLoader)
    {
        _workItems = new List<WorkItem>();
        _fileLoader = fileLoader;
        LoadFromFile();
    }
    public int GetCount()
    {
        return _workItems.Count;
    }
    public void Add(WorkItem item)
    {
        _workItems.Add(item);
    }
    private void LoadFromFile()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, "data.csv");
        // var lines = File.ReadAllLines(filePath);
        var lines = _fileLoader.LoadLines(filePath);
        foreach (var line in lines)
        {
            var columns = line.Split('|');
            _workItems.Add(new WorkItem
            {
                Id = Convert.ToInt32(columns[0]),
                Title = columns[1],
                Value = Convert.ToUInt32(columns[2]),
                Size = Enum.Parse<WorkItemSize>(columns[3]),
                IsCompleted = Convert.ToBoolean(columns[4])
            });
        }
    }
}
