using Application.Model;

namespace Application.Business;

internal class PaymentBusiness
{
    internal Result ProcessPayment(CreditCardPayment paymentInfo, ShoppingCart chart)
    {
        Console.WriteLine("Processing payment...");
        return new Result
        {
            IsSuccess = true,
            ErrorMessage = string.Empty
        };
    }

    internal bool ValidatePayment(CreditCardPayment paymentInfo)
    {
        Console.WriteLine("Validating payment...");
        if (string.IsNullOrEmpty(paymentInfo.CardNumber) || string.IsNullOrEmpty(paymentInfo.Cvv))
        {
            return false;
        }
        if (paymentInfo.ExpirationDate < DateTime.Now)
        {
            return false;
        }
        return true;
    }
}
