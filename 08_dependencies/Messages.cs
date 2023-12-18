namespace Sdk;

public enum Status
{
    Added,
    AlreadyExist,
    Deleted,
    Found,
    Updated,
    FileSaved
}

public class Result
{
    public string Message { get; set; } = string.Empty;
    public Status Status { get; set; }
}