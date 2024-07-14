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
        public class OrderService : IOrderService
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

        public async Task<HttpResponseMessage> CreateOrder(OrderDTO order)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/orders", order);
               

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error creating order: {response.StatusCode} - {errorContent}");
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HttpRequestException while creating order");
                throw; // Rethrow the exception to be handled higher up if needed
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while creating order");
                throw; // Rethrow the exception to be handled higher up if needed
            }
        }
        public async Task<HttpResponseMessage> CreateOrderItems(List<OrderItemDTO> orderItems)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orderitems", orderItems);
           
            response.EnsureSuccessStatusCode(); // Or implement your own error handling
            return response;
        }

        public async Task<HttpResponseMessage> CreatePayment(PaymentDTO payment)
        {
            var response = await _httpClient.PostAsJsonAsync("api/payments", payment);
            response.EnsureSuccessStatusCode(); // Or implement your own error handling
            return response;
        }
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
        }
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
 