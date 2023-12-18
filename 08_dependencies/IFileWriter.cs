namespace Sdk;
public interface IFileWriter<T>
    where T : class
{
    Result Write(string targetFile, List<T> source);
}