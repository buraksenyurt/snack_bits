namespace Application.Model
{
    internal class CreditCardPayment
    {
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Cvv { get; set; }
    }
}