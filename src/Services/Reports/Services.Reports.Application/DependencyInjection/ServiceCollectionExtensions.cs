using Microsoft.Extensions.DependencyInjection;
using Services.Reports.Domain.Factories;

namespace Services.Reports.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IReportFactory, ReportFactory>();

        return services;
    }
}