namespace SymphonyOfDestruction
{
    public abstract class Payment
    {
        public abstract decimal CalculatePrice(decimal price);
    }

    public class CreditCardPayment
        : Payment
    {
        public override decimal CalculatePrice(decimal price)
        {
            return price * 0.05M;
        }
    }

    public class CashPayment
        : Payment
    {
        public override decimal CalculatePrice(decimal price)
        {
            return price * 0.01M;
        }
    }

    public class PayPalPayment
        : Payment
    {
        public override decimal CalculatePrice(decimal price)
        {
            return price * 0.025M;
        }
    }

    public class SmartShoppingChart
    {
        public decimal CalculatePrice(Order order, Payment payment)
        {
            decimal total = 0;

            foreach (var item in order.Orders)
            {
                total += item.Price + payment.CalculatePrice(item.Price);
            }

            return total;
        }
    }
}
