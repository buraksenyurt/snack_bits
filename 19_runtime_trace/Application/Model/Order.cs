namespace Application.Model;

internal class Order
{
    public Customer Customer { get; set; }
    public PaymentType PaymentType { get; set; }
    public CreditCardPayment PaymentInfo { get; set; }
}
