using Application;
using Application.Services;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Chat;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Views.Utils.UIs;


namespace SignalChat_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddApplication();
            services.AddSingleton<UiState>();
            services.AddInfrastructure(configuration);
            services.AddScoped<App>();
          
            var provider = services.BuildServiceProvider();
            var app = provider.GetRequiredService<App>();
            await app.RunAsync();
        }
    }

}
