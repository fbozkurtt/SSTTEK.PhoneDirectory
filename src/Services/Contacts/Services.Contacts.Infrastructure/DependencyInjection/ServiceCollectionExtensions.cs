using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Contacts.Domain.Aggregates.Contact;
using Services.Contacts.Infrastructure.EF.Contexts;
using Services.Contacts.Infrastructure.EF.Options;
using Services.Contacts.Infrastructure.EF.Repositories;
using Services.Contacts.Infrastructure.Events;
using Services.Contacts.Infrastructure.Events.Options;
using Services.Contacts.Infrastructure.Logging;
using Shared.Abstractions.Commands;
using Shared.Abstractions.Events;
using Shared.DependencyInjection;
using Shared.Options;

namespace Services.Contacts.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IContactRepository, PostgresContactRepository>();

        var options = configuration.GetOptions<PostgresOptions>("Postgres");
        services.AddDbContext<ReadDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));
        services.AddDbContext<WriteDbContext>(ctx =>
            ctx.UseNpgsql(options.ConnectionString));

        return services;
    }

    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<MassTransitOptions>("MassTransit");

        services.AddMassTransit(c =>
        {
            c.UsingRabbitMq((context, cfg) =>
            {
                var uri = new Uri($"{options.Host}:{options.Port}");

                cfg.Host(uri, ch =>
                {
                    if (options.Username is null || options.Password is null)
                        return;
                    ch.Username(options.Username);
                    ch.Password(options.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });


        services.AddSingleton<IDomainEventPublisher, MassTransitDomainEventPublisher>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        services.AddMassTransit(configuration);
        services.AddQueries();

        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

        return services;
    }
}