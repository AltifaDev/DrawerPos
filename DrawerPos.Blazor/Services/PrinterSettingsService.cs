// Services/PrinterSettingsService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using DrawerPos.Shared;

namespace DrawerPos.Blazor.Services
{
    public class PrinterSettingsService
    {
        private readonly HttpClient _httpClient;

        public PrinterSettingsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PrinterSetting>> GetPrinterSettingsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<PrinterSetting>>("api/printersettings");
        }

        public async Task<PrinterSetting> GetPrinterSettingAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PrinterSetting>($"api/printersettings/{id}");
        }

        public async Task SavePrinterSettingAsync(PrinterSetting printerSetting)
        {
            if (printerSetting.Id == 0)
            {
                await _httpClient.PostAsJsonAsync("api/printersettings", printerSetting);
            }
            else
            {
                await _httpClient.PutAsJsonAsync($"api/printersettings/{printerSetting.Id}", printerSetting);
            }
        }

        public async Task DeletePrinterSettingAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/printersettings/{id}");
        }
    }
}
