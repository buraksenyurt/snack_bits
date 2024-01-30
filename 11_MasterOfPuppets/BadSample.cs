namespace MasterOfPuppets
{
    public class DataRepository
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine($"{order.Id} nolu sipariş oluşturuldu");
        }

        public void DeleteOrderItem(int orderId, int itemId)
        {
            Console.WriteLine($"{orderId} nolu siparişten {itemId} nolu kalem çıkartıldı");
        }

        public void GetSalesReport()
        {
            Console.WriteLine("Satış raporu hazırlandı");
        }
    }
}
