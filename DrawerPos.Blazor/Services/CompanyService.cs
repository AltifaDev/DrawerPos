// File: Services/CompanyService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrawerPos.Shared;

namespace DrawerPos.Blazor.Services
{
    public class CompanyService
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Company>>("api/companies");
        }

        public async Task<Company> GetCompanyByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Company>($"api/companies/{id}");
        }

        public async Task CreateCompanyAsync(Company company)
        {
            await _httpClient.PostAsJsonAsync("api/companies", company);
        }

        public async Task UpdateCompanyAsync(int id, Company company)
        {
            await _httpClient.PutAsJsonAsync($"api/companies/{id}", company);
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/companies/{id}");
        }
    }

}