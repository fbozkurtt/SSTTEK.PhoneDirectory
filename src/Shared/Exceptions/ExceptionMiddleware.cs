using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Shared.Abstractions.Exceptions;

namespace Shared.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (PhoneDirectoryException ex)
        {
            var exceptionName = ex.GetType().Name.Replace("Exception", string.Empty);
            context.Response.StatusCode = exceptionName.Contains("NotFound") ? 404 : 400;
            context.Response.Headers.Add("content-type", "application/json");

            var errorCode = ToUnderscoreCase(exceptionName);
            var json = JsonSerializer.Serialize(new {ErrorCode = errorCode, ex.Message});
            await context.Response.WriteAsync(json);
        }
    }

    private static string ToUnderscoreCase(string value)
    {
        return string.Concat(value.Select((x, i) =>
                i > 0 && char.IsUpper(x) && !char.IsUpper(value[i - 1]) ? $"_{x}" : x.ToString()))
            .ToLower(new CultureInfo("en-US"));
    }
}