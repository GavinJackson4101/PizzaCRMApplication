using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestBlazorAppGJackson;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using TestBlazorAppGJackson.Services;
using TestBlazorAppGJackson.Services.TestBlazorAppGJackson.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add the root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<UserService>();

// Register PizzaService with HttpClient
builder.Services.AddHttpClient<PizzaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5002/"); // Replace with your API base URL
});

// Register ToppingService with HttpClient (if needed)
builder.Services.AddHttpClient<ToppingService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5002/"); // Replace with your API base URL
});

// Register the default HttpClient if needed for other purposes
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
