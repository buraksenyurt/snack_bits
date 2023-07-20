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