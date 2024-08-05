using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TodoListBlazorWasm;
using TodoListBlazorWasm.Serivces;
using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<ITaskApiClient, TaskApiClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5001") });

await builder.Build().RunAsync();
