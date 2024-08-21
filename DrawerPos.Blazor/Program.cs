using CurrieTechnologies.Razor.SweetAlert2;
using DrawerPos.Blazor;
using DrawerPos.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
 

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add root components to the host
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with the correct base address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7141/") });
builder.Services.AddHttpClient<OrderService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7141/");
});
builder.Services.AddSweetAlert2();

// Add ProductService, CategoryService, and OrderService
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CompanyService>();
builder.Services.AddScoped<ReceiptHeaderService>();
builder.Services.AddScoped<PrinterSettingsService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<PromptPayService>();
builder.Services.AddScoped<MethodPaymentService>();
builder.Services.AddScoped<IngredientService>();
builder.Services.AddScoped<UnitService>();
builder.Services.AddScoped<IngredientStockService>();



// Configure logging
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Build and run the application
await builder.Build().RunAsync();