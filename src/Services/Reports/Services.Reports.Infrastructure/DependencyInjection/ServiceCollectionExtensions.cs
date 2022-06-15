using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Reports.Application.Events.Consumers;
using Services.Reports.Application.Services;
using Services.Reports.Domain.Entities.Report;
using Services.Reports.Infrastructure.EF.Contexts;
using Services.Reports.Infrastructure.EF.Options;
using Services.Reports.Infrastructure.EF.Repositories;
using Services.Reports.Infrastructure.EF.Services;
using Services.Reports.Infrastructure.Events.Options;
using Shared.DependencyInjection;
using Shared.Options;

namespace Services.Reports.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IReportRepository, PostgresReportRepository>();
        services.AddScoped<IReportReadService, PostgresReportReadService>();

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
            c.AddConsumer<ContactFieldAddedConsumer>();
            c.AddConsumer<ContactFieldRemovedConsumer>();
            c.AddConsumer<ContactDeletedConsumer>();
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

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgres(configuration);
        services.AddMassTransit(configuration);
        services.AddQueries();

        return services;
    }
}