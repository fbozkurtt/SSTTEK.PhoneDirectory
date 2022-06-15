using Microsoft.Extensions.DependencyInjection;
using Services.Contacts.Domain.Factories;
using Shared.DependencyInjection;

namespace Services.Contacts.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddSingleton<IContactFactory, ContactFactory>();
            
        return services;
    }
}