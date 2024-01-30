namespace SymphonyOfDestruction
{
    public class ShoppingChart
    {
        public decimal CalculatePrice(Order order)
        {
            decimal total = 0;

            foreach (var item in order.Orders)
            {
                switch (order.PaymentMethod)
                {
                    case PaymentMethod.CreditCard:
                        total += item.Price * 1.05M;
                        break;
                    case PaymentMethod.Cash:
                        total += item.Price * 1.01M;
                        break;
                }
            }

            return total;
        }
    }
}
