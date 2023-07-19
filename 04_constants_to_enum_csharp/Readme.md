# Constant'lardan Enum Sabitlerine

Çalışmakta olduğum legacy bir sistemde tablolarda tutulan sayısal değerlerin ismen karşılıkları kullanılmakta. Örneğin müşteri tipinin tablolarda tutulan sayısal karşılığının kod tarafında ismen bir anlamı var. 101 için Gold, 205 için Platinium gibi. Lakin kod modernizasyonu kapsamında constant'ların enum sabiti olarak değiştirilmesi gündeme geldi. Constant'lar sistem içinde birkaç static sınıfta durmaktaydı. Bu nedenle değişimi elle yapmak yerine Reflection ve IO işlemleri ile gerçekleştirebileceğimi düşündüm. Kobay bir static sınıf için aşağıdaki CustomerType tipini ele alabiliriz.

```csharp
namespace Domain
{

    public static class CustomerType
    {
        public const int Student = 1001;
        public const int Beginner = 1005;
        public const int Premium = 7007;
        public const int Employee = 9042;
    }
}
```

Amacımız bu static sınıftaki sabitleri barındıracak CustomerType isimli bir Enum sabitini kodla ürettirmek.