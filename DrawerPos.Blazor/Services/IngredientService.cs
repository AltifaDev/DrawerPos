using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DrawerPos.Shared;

namespace DrawerPos.Blazor.Services
{
    public class IngredientService
    {
        private readonly HttpClient _httpClient;

        public IngredientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Ingredient>>("api/Ingredients");
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ingredient>($"api/Ingredients/{id}");
        }

        public async Task AddIngredientAsync(IngredientCreateDto ingredientDto)
        {
            await _httpClient.PostAsJsonAsync("api/Ingredients/AddEdit", ingredientDto);
        }

        public async Task UpdateIngredientAsync(int id, IngredientCreateDto ingredientDto)
        {
            await _httpClient.PutAsJsonAsync($"api/Ingredients/AddEdit/{id}", ingredientDto);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Ingredients/{id}");
        }
    }
}
