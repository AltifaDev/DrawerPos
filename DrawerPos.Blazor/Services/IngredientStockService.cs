using DrawerPos.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DrawerPos.Blazor.Services
{
    public class IngredientStockService
    {
        private readonly HttpClient _httpClient;

        public IngredientStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IngredientStock>> GetIngredientStocksAsync()
        {
            // Ensure the URL is correct and matches your API
            return await _httpClient.GetFromJsonAsync<List<IngredientStock>>("api/IngredientStock");
        }

        public async Task<IngredientStock> GetIngredientStockByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IngredientStock>($"api/IngredientStock/{id}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                // Handle not found, maybe return null or a default value
                return null;
            }
        }
        public async Task CreateIngredientStockAsync(IngredientStock ingredientStock)
        {
            await _httpClient.PostAsJsonAsync("api/IngredientStock", ingredientStock);
        }

        public async Task UpdateIngredientStockAsync(int id, IngredientStock ingredientStock)
        {
            await _httpClient.PutAsJsonAsync($"api/IngredientStock/{id}", ingredientStock);
        }

        public async Task DeleteIngredientStockAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/IngredientStock/{id}");
        }
    }
}
