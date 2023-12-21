# Mocking İhtiyacını Görmek için Basit Bir Düzenek

Başlangıçta aşağıdaki gibi bir tasarımımız var.

```csharp
namespace KanbanWorld;

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
namespace KanbanWorld;

public interface IFileLoader
{
    IEnumerable<string> LoadLines(string filePath);
}

public class FileLoader
    : IFileLoader
{
    public IEnumerable<string> LoadLines(string filePath)
    {
        return File.ReadLines(filePath);
    }
}

public class Manager
{
    private readonly List<WorkItem> _workItems;
    private readonly IFileLoader _fileLoader;

    public Manager(IFileLoader fileLoader)
    {
        _workItems = [];
        _fileLoader = fileLoader;
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
        // var lines = File.ReadAllLines(filePath);
        var lines = _fileLoader.LoadLines(filePath);
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

Dikkat edilmesi gereken nokta verinin sağlandığı satır okuma işinin IFileLoader arayüzü ile Manager sınıfının dışına alınmasıdır. Bir başka deyişle bu bağımlılık Manager sınıfına yapıcı metot ile aktarılmakta ve LoadFromFile fonksiyonunda kullanılmaktadır. IFileLoader sınıfının LoadLines metodu geriye test metotları için gerekli içeriği döndürecek şekilde kurgulanabilir. Bunun için Mock kütüphanesinden yararlanılabilir. O nedenle test projesinde aşağıdaki komutla gereki ekleme işlemi yapılmaktadır.

```bash
dotnet add package Moq
```

Buna göre örnek test metotları aşağıdaki gibi oluşturulabilir.

```csharp
using KanbanWorld;
using Moq;

namespace _09_mocking_tests;

public class ManagerTests
{
    [Fact]
    public void GetCount_Returns_3_Test()
    {
        var mockFileLoader = new Mock<IFileLoader>();
        mockFileLoader.Setup(m => m.LoadLines(It.IsAny<string>())).Returns(new List<string>{
           "1|Haftasonu unit test konusuna bak|50|M|false",
           "2|Denizler Altında Yirmi Bin Fersah kitabının özetini çıkart|90|XL|false",
           "3|5 Km yürüyüş yap|100|S|true",
        });

        var manager = new Manager(mockFileLoader.Object);
        var actual = manager.GetCount();
        Assert.Equal(3, actual);
    }
}
```

Bu testte gerçek bir fiziki dosyadan yükleme yapmak yerine sanki o dosya varmış ve içeriği de 3 öğe ile doluymuş gibi ilerlenmesi sağlanıyor.