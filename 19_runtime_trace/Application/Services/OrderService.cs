using Application.Business;
using Application.Model;

namespace Application.Services;

internal class OrderService(CustomerBusiness customerBusiness, OrderBusiness orderBusiness, ChartBusiness chartBusiness, PaymentBusiness paymentBusiness)
{
    private readonly CustomerBusiness _customerBusiness = customerBusiness;
    private readonly OrderBusiness _orderBusiness = orderBusiness;
    private readonly ChartBusiness _chartBusiness = chartBusiness;
    private readonly PaymentBusiness _paymentBusiness = paymentBusiness;

    internal Result CreateOrder(Order order, ShoppingCart chart)
    {
        // Utility.LogStackTrace();

        if (!_customerBusiness.ValidateCustomer(order.Customer))
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Invalid customer information."
            };
        }

        if (!_chartBusiness.CheckStock(chart))
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Some items are out of stock."
            };
        }

        if (!_paymentBusiness.ValidatePayment(order.PaymentInfo))
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Invalid payment information."
            };
        }

        if (!_orderBusiness.Complete(order, chart))
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = "Failed to create order."
            };
        }

        var paymentResult = _paymentBusiness.ProcessPayment(order.PaymentInfo, chart);
        if (!paymentResult.IsSuccess)
        {
            return new Result
            {
                IsSuccess = false,
                ErrorMessage = paymentResult.ErrorMessage
            };
        }

        return new Result
        {
            IsSuccess = true,
            ErrorMessage = string.Empty,
            Data = new PaymentStatus
            {
                Amount = chart.GetTotalPrice(),
                OrderId = Guid.NewGuid().ToString()
            }
        };
    }
}
