namespace MasterOfPuppets
{
    public class SalesRepository
    {
        private readonly OrderManager orderManager;
        private readonly OrderItemManager orderItemManager;
        private readonly ReportManager reportManager;
        public SalesRepository()
        {
            orderManager = new OrderManager();
            orderItemManager = new OrderItemManager();
            reportManager = new ReportManager();
        }
        public void CreateOrder(Order order)
        {
            orderManager.CreateOrder(order);
        }

        public void DeleteOrderItem(int orderId, int itemId)
        {
            orderItemManager.DeleteOrderItem(orderId, itemId);
        }

        public void GetSalesReport()
        {
            reportManager.GetSalesReport();
        }
    }

    public class OrderManager
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine($"{order.Id} nolu sipariş oluşturuldu");
        }

    }

    public class OrderItemManager
    {
        public void DeleteOrderItem(int orderId, int itemId)
        {
            Console.WriteLine($"{orderId} nolu siparişten {itemId} nolu kalem çıkartıldı");
        }
    }

    public class ReportManager
    {
        public void GetSalesReport()
        {
            Console.WriteLine("Satış raporu hazırlandı");
        }
    }
}
