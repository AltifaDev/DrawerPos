// File: DrawerPos.Blazor.Services/OrderService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazorise;
using DrawerPos.Shared;
using Microsoft.Extensions.Logging;

namespace DrawerPos.Blazor.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(HttpClient httpClient, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<string> GetLastBillNoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/orders/last-billno");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching last BillNo. Response: {response}", ex.Message);
                throw;
            }
        }
        public async Task CreateOrder(Order order)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/orders", order);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating order. Response: {response}", ex.Message);
                throw;
            }
        }
    }
}
