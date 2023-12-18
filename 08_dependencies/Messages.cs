namespace AzonWorks;

public enum Status
{
    Added,
    AlreadyExist,
    Deleted,
    Found,
    Updated
}

public class Result
{
    public string Title { get; set; }
    public Status Status { get; set; }
}