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

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
        {
            services.AddHttpClient<IContacts, ContactsService>();
            services.AddHttpClient<IAuth, AuthClientService>();
            services.AddHttpClient<ISignalRClient, SignalRClient>();
            return services;
        }
    }
}
