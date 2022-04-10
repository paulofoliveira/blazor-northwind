using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NorthwindDemo.Client;
using NorthwindDemo.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration["ApiUrl"] ?? throw new Exception("Configuração de API não informada no AppSettings");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });
builder.Services.AddScoped<ICustomerDataService, CustomerDataService>();

builder.Services.AddBlazorise(options =>
   {
       options.Immediate = true;
   })
.AddBootstrap5Providers()
.AddFontAwesomeIcons();

await builder.Build().RunAsync();
