namespace _09_mocking;

public class WorkItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public uint Value { get; set; }
    public WorkItemSize Size { get; set; }
    public bool IsCompleted { get; set; } = false;
}
public enum WorkItemSize
{
    S,
    M,
    L,
    XL
}