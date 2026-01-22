using Application;
using Application.Services;
using Infrastructure;
using Infrastructure.Auth;
using Infrastructure.Chat;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Views;
using Spectre.Console;
using Spectre.Console.Rendering;

using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SignalChat_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
           var services = new ServiceCollection();

            services.AddApplication();
            services.AddInfrastructure();
            services.AddScoped<App>();
          
            var provider = services.BuildServiceProvider();
            var app = provider.GetRequiredService<App>();
            await app.RunAsync();
        }
    }

}
