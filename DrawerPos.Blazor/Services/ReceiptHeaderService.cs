using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrawerPos.Shared;
using System.Linq;
using System;

namespace DrawerPos.Blazor.Services
{
    public class ReceiptHeaderService
    {
        private readonly HttpClient _httpClient;

        public ReceiptHeaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ReceiptHeader?> GetLatestReceiptHeaderAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/ReceiptHeaders/latest");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ReceiptHeader>();
                }
                else
                {
                    // Handle not found scenario
                    Console.WriteLine($"API call failed with status code {response.StatusCode}");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it
                Console.WriteLine($"Request error: {ex.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<ReceiptHeader>> GetReceiptHeadersAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<ReceiptHeader>>("api/ReceiptHeaders");
            }
            catch (Exception ex)
            {
                // Log the error (not implemented in this example)
                throw new ApplicationException("Error fetching receipt headers", ex);
            }
        }

        public async Task<ReceiptHeader> GetReceiptHeaderByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ReceiptHeader>($"api/ReceiptHeaders/{id}");
            }
            catch (Exception ex)
            {
                // Log the error (not implemented in this example)
                throw new ApplicationException($"Error fetching receipt header with id {id}", ex);
            }
        }

        public async Task CreateReceiptHeaderAsync(ReceiptHeader receiptHeader)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/ReceiptHeaders", receiptHeader);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Log the error (not implemented in this example)
                throw new ApplicationException("Error creating receipt header", ex);
            }
        }

        public async Task UpdateReceiptHeaderAsync(int id, ReceiptHeader receiptHeader)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/ReceiptHeaders/{id}", receiptHeader);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Log the error (not implemented in this example)
                throw new ApplicationException($"Error updating receipt header with id {id}", ex);
            }
        }

        public async Task DeleteReceiptHeaderAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ReceiptHeaders/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Log the error (not implemented in this example)
                throw new ApplicationException($"Error deleting receipt header with id {id}", ex);
            }
        }
    }
}
