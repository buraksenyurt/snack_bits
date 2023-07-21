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
using Domain;
using Business;
using Ordering;

var cpuOrder = new OrderInfo
{
    OrderNo = 90123,
    SourceLocation = "İzmir",
    TargetLocation = "London",
    Quantity = 1500
};

var costManager = new ShipmentManager();
var cost = costManager.CalculateCost(ShipmentType.ViaPlane, cpuOrder);
Console.WriteLine($"Uçakla taşıma maliyeti {cost} birim");

cost = costManager.CalculateCost(ShipmentType.ViaShip, cpuOrder);
Console.WriteLine($"Gemiyle taşıma maliyeti {cost} birim");

cost = costManager.CalculateCost(ShipmentType.ViaTrain, cpuOrder);
Console.WriteLine($"Trenle taşıma maliyeti {cost} birim");

namespace Domain
{
    public enum ShipmentType
    {
        ViaShip,
        ViaPlane,
        ViaTrain,
        ViaTruck
    }
    public class OrderInfo
    {
        public int OrderNo { get; set; }
        public int Quantity { get; set; }
        public string SourceLocation { get; set; }
        public string TargetLocation { get; set; }
    }
}

namespace Ordering
{
    using Business;
    using Domain;
    public class ShipmentManager
    {
        private ShipmentCostFactory factory = new ShipmentCostFactory();
        public ShipmentManager()
        {
            // HANDİKAP!
            //
            // Bu yükleme işinide dışarıya almak iyi bir çözüm olabilir.
            // Nitekim şu anda yeni bir araç için maliyet hesabı eklemek istersek
            // evet CalculateHost'a bir block açmıyoruz ama buraya
            // yeni bir factory.Add koymak zorunda kalıyoruz.
            factory.Add(ShipmentType.ViaShip, new Ship());
            factory.Add(ShipmentType.ViaPlane, new Plane());
            factory.Add(ShipmentType.ViaTrain, new Train());
            factory.Add(ShipmentType.ViaTruck, new Truck());
        }
        public decimal CalculateCost(ShipmentType shipmentType, OrderInfo orderInfo)
        {
            #region İkinci Sürüm

            var accounter = factory.Resolve(ShipmentType.ViaShip);
            return accounter.Calculate(orderInfo);

            #endregion

            #region İlk Sürüm

            // decimal calculatedValue = 1.0M;
            // switch (shipmentType)
            // {
            //     case ShipmentType.ViaShip:
            //         // orderInfo için bir şeylere bakılır
            //         calculatedValue = 33.5M;
            //         break;
            //     case ShipmentType.ViaPlane:
            //         // orderInfo için bir şeylere bakılır
            //         calculatedValue = 85.5M;
            //         break;
            //     case ShipmentType.ViaTrain:
            //         // orderInfo için bir şeylere bakılır
            //         calculatedValue = 5.5M;
            //         break;
            //     case ShipmentType.ViaTruck:
            //         // orderInfo için bir şeylere bakılır
            //         calculatedValue = 3.5M;
            //         break;
            // }
            // return calculatedValue;

            #endregion
        }
    }
}

namespace Business
{
    using Domain;
    public interface IShipmentCost
    {
        decimal Calculate(OrderInfo orderInfo);
    }
    public class Ship
        : IShipmentCost
    {
        public decimal Calculate(OrderInfo orderInfo)
        {
            // Burada farklı işlemler yapılıyor tabii
            // ürün tipine bak, miktara bak vs
            return 33.5M;
        }
    }
    public class Plane
        : IShipmentCost
    {
        public decimal Calculate(OrderInfo orderInfo)
        {
            // Burada farklı işlemler yapılıyor tabii
            // ürün tipine bak, miktara bak vs
            return 88.5M;
        }
    }
    public class Train
        : IShipmentCost
    {
        public decimal Calculate(OrderInfo orderInfo)
        {
            // Burada farklı işlemler yapılıyor tabii
            // ürün tipine bak, miktara bak vs
            return 5.5M;
        }
    }
    public class Truck
        : IShipmentCost
    {
        public decimal Calculate(OrderInfo orderInfo)
        {
            // Burada farklı işlemler yapılıyor tabii
            // ürün tipine bak, miktara bak vs
            return 3.5M;
        }
    }
    public class ShipmentCostFactory
    {
        private Dictionary<ShipmentType, IShipmentCost> options = new Dictionary<ShipmentType, IShipmentCost>();
        public void Add(ShipmentType shipmentType, IShipmentCost option)
        {
            options.Add(shipmentType, option);
        }
        public IShipmentCost Resolve(ShipmentType shipmentType)
        {
            options.TryGetValue(shipmentType, out IShipmentCost option);
            return option;
        }
        public void Remove(ShipmentType shipmentType)
        {
            options.Remove(shipmentType);
        }
    }
}
```

Kod biraz daha uzadı ve aslında şu an için bir handikapı var. Enum sabitleri ile karşılık olan nesne bağımlılıklarını ShipmentManager construtor fonksiyonunda kodla yüklüyoruz. Bir Dictionary kullanmaktayız. Switch...case bloğunu bertaraf ettik ama yeni bir taşıma yöntemine göre maliyet hesaplaması ihtiyacı olduğunda yine ShipmentManager koduna müdahale etmememiz gerekiyor. Dolayısıyla enum-nesne ilişkilerini taşıyan bağımlılıkların yüklenme işini farklı bir modüle almalı ya da farklı bir şekilde entegre etmeliyiz. Bir DI Container aracı (Windsor Castle, Ninject, Autofac vs) burada iş görebilir ya da built-in DI mekanizması ya da bir konfigurasyon dosyasından yüklenmeleri sağlanabilir. Örneğin bağımlı nesneleri interface ve nesne eşleri olarak bir JSON dosyasında tutup oradan yükleme yoluna da gidebiliriz - ben buna bir bakayım...

## ve Baktım

İlk etapta eski dostlardan Ninject ile isim çözümlemesi yolunu kullanarak ilermekeye çalıştım ancak çalışma zamanında Resolve işlemi sırasında Exception aldım. Biraz kurcaladıktan sonra Autofac ile ilerlemeye devam etti ve 3ncü sürüm ortaya çıktı. Bağımlı nesnelerin eklenmesi için Core isim alanı altında DependencyInjection isimli bir sınıf yer alıyor. Bu sınıfta Autofac Container nesnesini kullanarak ihtiyaç olunan servis nesnesinin isimle çekilmesini sağlayabiliyoruz.

```csharp
namespace Core
{
    public static class DependencyInjection
    {
        private static ContainerBuilder builder;
        private static IContainer container;
        static DependencyInjection()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<Ship>().Named<IShipmentCost>(ShipmentType.ViaShip.ToString());
            builder.RegisterType<Plane>().Named<IShipmentCost>(ShipmentType.ViaPlane.ToString());
            builder.RegisterType<Truck>().Named<IShipmentCost>(ShipmentType.ViaTruck.ToString());
            builder.RegisterType<Train>().Named<IShipmentCost>(ShipmentType.ViaTrain.ToString());
            container = builder.Build();

        }
        public static T GetByName<T>(string name)
        {
            return container.ResolveNamed<T>(name);
        }
    }
}

// Kullanım şekli
namespace Ordering
{
    using Business;
    using Core;
    using Domain;
    public class ShipmentManager
    {
        public decimal CalculateCost(ShipmentType shipmentType, OrderInfo orderInfo)
        {
            var instance = DependencyInjection.GetByName<IShipmentCost>(shipmentType.ToString());
            return instance.Calculate(orderInfo);
        }
    }
}
```

İşin esprisi servisleri register ederken Named fonksiyonu ile benzersiz isimler vererek (örnekte ShipmentCost enum sabitini adıdır) ilerlememiz. Buna göre CalculateHost fonksiyonuna gelen enum sabitinin adından yararlanarak register edilen servis nesnesini elde etmemiz mümkün oluyor.