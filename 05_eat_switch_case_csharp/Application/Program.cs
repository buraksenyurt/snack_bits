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