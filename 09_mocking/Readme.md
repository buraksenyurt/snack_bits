# Mocking İhtiyacını Görmek için Basit Bir Düzenek

Başlangıçta aşağıdaki gibi bir tasarımımız var.

```csharp
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

public class Manager
{
    private readonly List<WorkItem> _workItems;

    public Manager()
    {
        _workItems = [];
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
        var lines = File.ReadAllLines(filePath);
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
```

Manager sınıfı WorkItem nesne örneklerini yönetmek için bazı fonksiyonellikler sunuyor. Dikkat edilmesi gereken nokta LoadFromFile metodu. Bu sınıf için birim testler yazmak istediğimizde burası problem olabilir. Nitekim testin koşulduğu ortamda data.csv isimli bir dosya bulunmayabilir. Ya da bu dosya olup içerisindeki veri seti istediği gibi sağlanamayan ve bu sebeple patlayan testler olabilir. Örneğin yüklenen WorkItem listesinden sadece IsCompleted olanların bulunduğunu test eden bir metot data.csv yüklense bile hiçbir tanesi IsCompleted değilse hatalı sonuçlanır. Bir başka deyişle CSV içeriğini okuma işini mock nesne olarak tasarlamamız gerekiyor. Bunun üzerine ilgili sınıf içeriği aşağıdaki gibi değiştirilmelidir.

```csharp
```