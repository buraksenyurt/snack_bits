namespace Sdk;
public interface IWriter<T>
    where T : class, IEntity
{
    Result Write(string fileName, List<T> source);
}