using DrawerPos.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DrawerPos.Blazor.Services
{
    public class DashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>("api/orders");
        }


        public async Task<List<GroupedOrderItem>> GetDateOrders(DateTime? startDate, DateTime? endDate)
        {
            var response = await _httpClient.GetAsync($"api/dashboard/GetDateOrders?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<GroupedOrderItem>>();
        }

        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenue()
        {
            return await _httpClient.GetFromJsonAsync<List<MonthlyRevenueDto>>("api/dashboard/monthly-revenue");
        }
    }
}
