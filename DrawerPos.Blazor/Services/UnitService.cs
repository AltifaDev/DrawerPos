using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DrawerPos.Shared;
using System.Collections.Generic;
using System;


namespace DrawerPos.Blazor.Services
{
    public class UnitService
    {
        private readonly HttpClient _httpClient;

        public UnitService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Unit>> GetUnitsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Unit>>("api/Unit");
        }
        public async Task<Unit> GetUnitByIdAsync(int unitId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/units/{unitId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Unit>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw new Exception("Failed to retrieve unit");
            }
        }
    }
}
