using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using DrawerPos.Shared;

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

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/orders");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<IEnumerable<Order>>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                throw;
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                throw;
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GroupedOrderItem>> GetDateOrders(DateTime? startDate, DateTime? endDate)
        {
            var query = new Dictionary<string, string>();

            if (startDate.HasValue)
            {
                query["startDate"] = startDate.Value.ToString("yyyy-MM-dd");
            }

            if (endDate.HasValue)
            {
                query["endDate"] = endDate.Value.ToString("yyyy-MM-dd");
            }

            var queryString = string.Join("&", query.Select(kvp => $"{kvp.Key}={kvp.Value}"));
            var uri = $"api/orders/GetDateOrders?{queryString}";

            try
            {
                _logger.LogInformation($"Requesting data from: {uri}");
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupedOrderItem>>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<MonthlyRevenueDto>> GetMonthlyRevenue()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/orders/monthly-revenue");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<IEnumerable<MonthlyRevenueDto>>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                return Array.Empty<MonthlyRevenueDto>();
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                return Array.Empty<MonthlyRevenueDto>();
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                return Array.Empty<MonthlyRevenueDto>();
            }
        }

        // Method to create an order
        public async Task CreateOrder(Order order)
        {
            try
            {
                // Validate the order object
                if (order == null)
                {
                    _logger.LogError("Order object is null.");
                    throw new ArgumentNullException(nameof(order));
                }

                _logger.LogInformation($"Creating order: {JsonSerializer.Serialize(order)}");

                var response = await _httpClient.PostAsJsonAsync("api/orders", order);

                if (!response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error creating order. Status Code: {response.StatusCode}, Response: {responseContent}");
                    response.EnsureSuccessStatusCode(); // This will throw an exception
                }

                _logger.LogInformation("Order created successfully.");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error creating order. Message: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating order. Message: {Message}", ex.Message);
                throw;
            }
        }

        // Method to fetch the last generated BillNo
        public async Task<string> GetLastBillNoAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/orders/last-billno");
                _logger.LogInformation($"Received response: {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response body: {responseBody}");
                return responseBody;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error fetching last BillNo. Status code: {ex.StatusCode}, Response: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        // Method to fetch the most recent order
        public async Task<Order> GetMostRecentOrder()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/orders/most-recent");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<Order>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                throw;
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                throw;
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        public async Task<WeeklyMonthlyRevenueDto> GetWeeklyMonthlyRevenue()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/orders/weekly-monthly-revenue");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<WeeklyMonthlyRevenueDto>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                throw; // Propagate the exception to handle it higher up in the call stack
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                throw;
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                throw; // Propagate the exception for centralized error handling
            }
        }

        // Other methods...
    }

    public class WeeklyMonthlyRevenueDto
    {
        public List<DailyRevenueDto> LastWeekRevenue { get; set; }
        public List<DailyRevenueDto> ThisMonthRevenue { get; set; }
    }

    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class MonthlyRevenueDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
