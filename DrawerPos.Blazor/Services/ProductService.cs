using DrawerPos.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace DrawerPos.Blazor.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync("API/Products");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>(options);

                _logger.LogInformation($"Number of products received: {products?.Count() ?? 0}");

                return products ?? Array.Empty<Product>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HttpRequestException: {ex.Message}");
                return Array.Empty<Product>();
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"NotSupportedException: {ex.Message}");
                return Array.Empty<Product>();
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"JsonException: {ex.Message}");
                return Array.Empty<Product>();
            }
            catch (Exception ex) // Catch-all for other exceptions
            {
                _logger.LogError($"Exception: {ex.Message}");
                return Array.Empty<Product>();
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"API/Products/{id}");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<Product>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                return null;
            }
            catch (NotSupportedException ex)
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                return null;
            }
        }

        public async Task CreateProduct(Product product)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("API/Products", product);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"API/Products/{product.ProductId}", product);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"API/Products/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync("API/Categories");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<IEnumerable<Category>>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                return Array.Empty<Category>();
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                return Array.Empty<Category>();
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                return Array.Empty<Category>();
            }
        }
    }
}
