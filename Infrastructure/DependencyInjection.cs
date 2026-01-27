using Infrastructure.Contacts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.Contacts;
using Application.Interfaces.Auth;
using Infrastructure.Auth;
using Application.Interfaces.Chat;
using Infrastructure.Chat;
using Application.Services;
using Application.Events;
using Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiOptions>(configuration.GetSection("Api"));
            services.AddHttpClient<IContacts, ContactsService>((sp, client)
                =>
            {
                var options = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl + "contacts/");
            });

            services.AddHttpClient<IAuth, AuthClientService>((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<ApiOptions>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl + "auth/");
            });
            services.AddSingleton<ISignalRClient, SignalRClient>();
            services.AddSingleton<ChatService>();
            
            return services;
        }
    }
}
