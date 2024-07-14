using DrawerPos.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DrawerPos.Blazor.Services
{
    public interface IOrderService
    {
        Task<HttpResponseMessage> CreateOrder(OrderDTO order);
        Task<IEnumerable<Order>> GetOrders();
        Task<List<GroupedOrderItem>> GetDateOrders(DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<MonthlyRevenueDto>> GetMonthlyRevenue();
        Task<string> GetLastBillNoAsync();
        Task<Order> GetMostRecentOrder();
        Task<WeeklyMonthlyRevenueDto> GetWeeklyMonthlyRevenue();
        Task<HttpResponseMessage> CreateOrderItems(List<OrderItemDTO> orderItems); // Add this method
        Task<HttpResponseMessage> CreatePayment(PaymentDTO payment); // Add this method

    }
}