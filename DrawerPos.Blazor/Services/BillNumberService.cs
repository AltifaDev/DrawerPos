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
    public class BillNumberService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BillNumberService> _logger;

        public BillNumberService(HttpClient httpClient, ILogger<BillNumberService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<BillNumber>> GetBillNumber()
        {
            try
            {
                var response = await _httpClient.GetAsync("API/BillNumber");
                response.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                return await response.Content.ReadFromJsonAsync<IEnumerable<BillNumber>>(options);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Request error: {ex.Message}");
                return Array.Empty<BillNumber>();
            }
            catch (NotSupportedException ex) // When content type is not valid
            {
                _logger.LogError($"The content type is not supported: {ex.Message}");
                return Array.Empty<BillNumber>();
            }
            catch (JsonException ex) // Invalid JSON
            {
                _logger.LogError($"Invalid JSON: {ex.Message}");
                return Array.Empty<BillNumber>();
            }
        }
    }
}
