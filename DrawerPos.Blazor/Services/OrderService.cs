// File: Services/OrderService.cs

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DrawerPos.Shared;

namespace DrawerPos.Blazor.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", order);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Order>();
        }
    }
}
