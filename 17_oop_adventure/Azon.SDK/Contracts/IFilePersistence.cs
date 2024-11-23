namespace Azon.SDK.Contracts
{
    public interface IFilePersistence : IPersistence
    {
        string FileName { get; set; }
        string FileType { get; }
    }
}
