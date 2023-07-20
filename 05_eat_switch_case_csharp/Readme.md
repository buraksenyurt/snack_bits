# Eat Switch Case Blocks

Cognitive Complexity değerlerinin yüksek olduğu fonksiyonlarla mücadele ederken çoğunlukla switch...case ve if...else gibi yapıları dışarıya almaya çalışıyorum. Buradaki kod bloklarının fonksiyon akışı içerisinde state değişikliği yapan birer strateji olduğunu düşünüyorum...Genellikle karşılaştığım problemler bu yönde düşünmeme neden oluyor. Esasında diğer birçok örnekte de benzer durumlarla mücadele için bir takım yöntemlere başvurdum. DI Container'lar bu anlamada önemli başucu kaynakları diyebilirim. Bu örnekte ise çok temel seviyede switch...case kurgusundan nasıl kurtuluruz onu incelemek istedim. Senaryomuz aşağıdaki kod parçası ile başlıyor.

```csharp
OrderInfo cpuOrder = new OrderInfo
{
    OrderNo = 90123,
    SourceLocation = "İzmir",
    TargetLocation = "London",
    Quantity = 1500
};

var costManager = new ShipmentManager();
var cost = costManager.CalculateCost(ShipmentType.ViaPlane, cpuOrder);
Console.WriteLine($"Taşıma maliyet {cost} birim");

enum ShipmentType
{
    ViaShip,
    ViaPlane,
    ViaTrain,
    ViaTruck
}
class OrderInfo
{
    public int OrderNo { get; set; }
    public int Quantity { get; set; }
    public string SourceLocation { get; set; }
    public string TargetLocation { get; set; }
}

class ShipmentManager
{
    public decimal CalculateCost(ShipmentType shipmentType, OrderInfo orderInfo)
    {
        decimal calculatedValue = 1.0M;
        switch (shipmentType)
        {
            case ShipmentType.ViaShip:
                // orderInfo için bir şeylere bakılır
                calculatedValue = 33.5M;
                break;
            case ShipmentType.ViaPlane:
                // orderInfo için bir şeylere bakılır
                calculatedValue = 85.5M;
                break;
            case ShipmentType.ViaTrain:
                // orderInfo için bir şeylere bakılır
                calculatedValue = 5.5M;
                break;
            case ShipmentType.ViaTruck:
                // orderInfo için bir şeylere bakılır
                calculatedValue = 3.5M;
                break;
        }
        return calculatedValue;
    }
}
```

Güya bir siparişin alınacağı konumdan gitmesi gereken konuma doğru taşınması için gerekli maliyeti hesaplayan bir fonksiyonelliğimiz var. Fonksiyon taşımanın neyle yapılacağına, taşınacak miktara, ürün bilgisine göre hesaplanıyor. Burada ana kriter taşıma tipi. Uçak, tren, tır veya gemi gibi seçenekler var. Programın çalışmasında bir sıkıntı yok. İşini yapıyor. Ancak farklı bir taşıma yöntemi(Helikopter :P) belki birkaç taşıma tekniğinin kombinasyonu eklenmek istenirse. Bu durumda fonksiyonda yeni bir case bloğu açmamız gerekiyor. Amaç bu stratejiyi dışarıdan edinip kullandırmak. CalculateCost fonksiyonuna müdahale etmeye gerek kalmadan. Normalda bunun yöntemleri belli. Repodaki birkaç örnekte bunlara yer verdim. Bu sefer farklı bir teknik kullanalım.

```csharp
// Çözüm gelecek
```