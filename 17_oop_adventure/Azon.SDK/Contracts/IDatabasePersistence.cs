namespace Azon.SDK.Contracts
{
    public interface IDatabasePersistence : IPersistence
    {
        string ConnectionString { get; }
    }
}
