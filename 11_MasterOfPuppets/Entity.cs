namespace MasterOfPuppets
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderItem> Orders { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
