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
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://drawerposapi.runasp.net/") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7141/") });
builder.Services.AddSweetAlert2();

// Add ProductService and CategoryService
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();

// Configure logging
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Build and run the application
await builder.Build().RunAsync();
