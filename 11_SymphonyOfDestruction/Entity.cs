namespace SymphonyOfDestruction
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderItem> Orders { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }

    public enum PaymentMethod
    {
        Cash,
        CreditCard
    }
}
