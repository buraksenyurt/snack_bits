# Constant'lardan Enum Sabitlerine

Çalışmakta olduğum legacy bir sistemde tablolarda tutulan sayısal değerlerin ismen karşılıkları kullanılmakta. Örneğin müşteri tipinin tablolarda tutulan sayısal karşılığının kod tarafında ismen bir anlamı var. 101 için Gold, 205 için Platinium gibi. Lakin kod modernizasyonu kapsamında constant'ların enum sabiti olarak değiştirilmesi gündeme geldi. Constant'lar sistem içinde birkaç static sınıfta durmaktaydı. Bu nedenle değişimi elle yapmak yerine Reflection ve IO işlemleri ile gerçekleştirebileceğimi düşündüm. Kobay bir static sınıf için aşağıdaki ProcessType tipini ele alabiliriz.

```csharp
namespace Domain
{

    public static class ProcessType
    {
        public const int Student = 1001;
        public const int Beginner = 1005;
        public const int Premium = 7007;
        public const int Employee = 9042;
    }
}
```

Amacımız bu static sınıftaki sabitleri barındıracak ProcessTypeEnum isimli bir Enum sabitini kodla ürettirmek. İlk etapta aşağıdaki gibi yarı otomatize bir çözüm kullanabiliriz.

```csharp
using System.Reflection;
using System.Text;
using Domain;
public class Program
{
    private static void Main(string[] args)
    {
        var catcher = new EnumCatcher();
        var content = catcher.Apply(typeof(ProcessType));
        File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "Output.cs"), content.ToString());
    }
}

public class EnumCatcher
{
    public string Apply(Type sourceType)
    {
        var fields = sourceType
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly);

        var builder = new StringBuilder();
        builder.AppendLine($"public enum {nameof(ProcessType)}Enum");
        builder.AppendLine("{");
        foreach (var field in fields)
        {
            builder.AppendLine($"\t{field.Name} = {sourceType.GetField(field.Name).GetValue(null)},");
        }
        builder.AppendLine("}");
        return builder.ToString();
    }
}

namespace Domain
{
    public static class ProcessType
    {
        public const int Student = 1001;
        public const int Beginner = 1005;
        public const int Premium = 7007;
        public const int Employee = 9042;
    }
}
```

EnumCatcher sınıfının Apply metodu gelen tip alınıp reflection ile taranırken Constant alanlar ele alınır ve bir StringBuilder'dan da yararlanılarak çıktı olan enum tipinin text tabanlı oluşturulması sağlanır. Bir kısmı otomatize bir kısmı yine elle müdahaleyi gerektirmekte. Şöyleki; legacy sistemdeki dönüştürülmesi gereken tipleri biliyorsak yine proje içerisindeki bir kod parçasında bunların karşılığı olan enum türlerinin ürettirilmesi ve elde edilen dosyasının solution'a monte edilmesi, sonrasında constant'ları içeren sınıfların silinerek enum sabiti kullanımları ile değiştirilmesi gerekir. Enum'ların kodla üretimi dışındaki kısımlar manuel operasyon gerektirdiğinden hataya da oldukça açıktır ama daha da önemlisi zaman kaybına neden olur. Daha iyi bir çözüm .Net Framework tarafında Roslyn kullanarak Solution üstündeki değişiklikleri tamamen otomatize etmekten geçer. Ancak uyguladığım çözüm şimdilik günümü kurtarmama yetti diyebilirim.