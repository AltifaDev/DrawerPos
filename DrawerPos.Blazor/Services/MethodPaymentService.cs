using System.Net.Http.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DrawerPos.Shared;
using System.Text.Json;
using System.Text.RegularExpressions;
using System;
using System.Text.Json.Serialization;

namespace DrawerPos.Blazor.Services
{
    public class MethodPaymentService
    {
        private readonly HttpClient _httpClient;

        public MethodPaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MethodPayment>> GetMethodPaymentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MethodPayment>>("API/methodpayment");
        }

        public async Task<List<MethodPayment>> GetMethodPaymentsWithStatusYesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MethodPayment>>("API/methodpayment/PaymentsStatus");
            
        }

        public async Task<MethodPayment> GetMethodPaymentByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<MethodPayment>($"API/methodpayment/{id}");
        }

        public async Task CreateMethodPaymentAsync(MethodPayment methodPayment)
        {
            await _httpClient.PostAsJsonAsync("API/methodpayment", methodPayment);
        }

        public async Task UpdateMethodPaymentAsync(int id, MethodPayment methodPayment)
        {
            await _httpClient.PutAsJsonAsync($"API/methodpayment/{id}", methodPayment);
        }

        public async Task DeleteMethodPaymentAsync(int id)
        {
            await _httpClient.DeleteAsync($"API/methodpayment/{id}");
        }
    }
}