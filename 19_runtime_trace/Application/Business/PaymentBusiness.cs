using Application.Model;
using Application.Trace;

namespace Application.Business;

internal class PaymentBusiness
{
    [MethodTrace]
    internal Result ProcessPayment(CreditCardPayment paymentInfo, ShoppingCart chart)
    {
        Console.WriteLine("Processing payment...");
        return new Result
        {
            IsSuccess = true,
            ErrorMessage = string.Empty
        };
    }

    [MethodTrace]
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
