namespace MasterOfPuppets
{
    static class Program
    {
        static void Main()
        {
            #region Bad Usage

            DataRepository dataRepository = new();
            dataRepository.CreateOrder(new Order
            {
                Id = 1,
                Date = DateTime.Now,
                Orders = [
                    new OrderItem { Id = 1001, ProductId = 98, Quantity = 10 },
                    new OrderItem { Id = 1002, ProductId = 55, Quantity = 5 }
             ]
            });
            dataRepository.DeleteOrderItem(1, 1001);
            dataRepository.GetSalesReport();

            #endregion

            #region Refactored

            SalesRepository salesRepository = new SalesRepository();
            salesRepository.CreateOrder(new Order
            {
               Id = 2,
               Date = DateTime.Now.AddDays(-3),
               Orders = [
                   new OrderItem { Id = 1001, ProductId = 98, Quantity = 10 },
                   new OrderItem { Id = 1002, ProductId = 55, Quantity = 5 },
                   new OrderItem { Id = 1003, ProductId = 10, Quantity = 1 },
               ]
            });
            salesRepository.DeleteOrderItem(2, 1002);
            salesRepository.GetSalesReport();

            #endregion
        }
    }
}
