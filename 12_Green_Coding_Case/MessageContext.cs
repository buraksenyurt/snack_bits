namespace MarineCorp.Erp.Model;

public class Context
{
    private readonly Dictionary<string, object> _elements;

    public Context()
    {
        _elements = new Dictionary<string, object>();
    }

    public void Add(string key, object value)
    {
        _elements.Add(key, value);
    }

    public object Get(string key)
    {
        return _elements.FirstOrDefault(e => e.Key == key);
    }
}