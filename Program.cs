using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorStrap;
using TodosSpa.Services.Contracts;
using TodosSpa.Services.Implementation;
using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;

namespace TodosSpa
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddScoped<IdentityAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthStateProvider>());
            builder.Services.AddScoped<IAuthorizationApi, AuthorizationApi>();
            builder.Services.AddScoped<ITaskManagerApi, TaskManagerApi>();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddStorage();

            builder.Services.AddBootstrapCss();

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
