using Microsoft.AspNetCore.Builder;
using Shared.Exceptions;

namespace Shared.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseShared(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}