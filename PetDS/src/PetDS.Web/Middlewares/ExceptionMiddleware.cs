using System.Net;
using Framework.Response;
using PetDS.Domain.Shered;
using SharedKernel.Exseption;

namespace PetDS.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    public readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Error error = Error.Conflict("Exception.server", ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(ex, "неизвестная ошибка из Middleware");
            await context.Response.WriteAsJsonAsync(Envelope.Error(error));
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();
}