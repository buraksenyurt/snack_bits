namespace Application;

internal class Result
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
    public object Data { get; set; }
}