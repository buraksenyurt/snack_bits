namespace SymphonyOfDestruction
{
    internal class Program
    {
        static void Main()
        {
            var orders = new List<OrderItem>(){
            new() {
                OrderId=1,
                ProductId=10,
                Price=26.90M
            },
            new() {
                OrderId=2,
                ProductId=9,
                Price=5.90M
            },
            new() {
                OrderId=3,
                ProductId=12,
                Price=8.50M
            },
        };
            var order = new Order
            {
                OrderId = 1,
                Orders = orders,
                PaymentMethod = PaymentMethod.CreditCard
            };

            #region Bad Practice

            var chart = new ShoppingChart();
            var totalPrice = chart.CalculatePrice(order);
            Console.WriteLine(totalPrice);

            #endregion

            #region Refactored Practice

            var smartChart = new SmartShoppingChart();
            totalPrice = smartChart.CalculatePrice(order, new CreditCardPayment());
            Console.WriteLine(totalPrice);

            #endregion
        }
    }
}
