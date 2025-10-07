using PetDS.Domain.Shered;
using PetDS.Web.Middlewares;
using PetDS.Web.Response;
using System.Net;

namespace PetDS.Web.Middlewares
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

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
                var error = Error.Conflict("Exception.server", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex, "неизвестная ошибка из Middleware");
                await context.Response.WriteAsJsonAsync(Envelope.Error(error));
            }
        }
    }
}
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
