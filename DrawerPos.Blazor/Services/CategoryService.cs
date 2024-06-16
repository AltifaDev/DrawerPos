using DrawerPos.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace DrawerPos.Blazor.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(HttpClient httpClient, ILogger<CategoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
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

        public async Task<Category> GetCategory(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"API/Categories/{id}");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<Category>(options);
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

        public async Task CreateCategory(Category category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("API/Categories", category);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }

        public async Task UpdateCategory(Category category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"API/Categories/{category.CategoryId}", category);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }

        public async Task DeleteCategory(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"API/Categories/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
            }
        }
    }
}
