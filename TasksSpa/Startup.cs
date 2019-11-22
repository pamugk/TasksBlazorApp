using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using TasksSpa.Services.Contracts;
using TasksSpa.Services.Implementation;

namespace TasksSpa
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddStorage();
            services.AddScoped<IdentityAuthStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthStateProvider>());
            services.AddScoped<IAuthorizationApi, AuthorizationApi>();
            services.AddScoped<ITaskManagerApi, TaskManagerApi>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
