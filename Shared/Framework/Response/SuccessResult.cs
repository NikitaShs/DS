using Microsoft.AspNetCore.Http;

namespace Framework.Response;

public class SuccessResult<T> : IResult
{
    private readonly T _result;

    public SuccessResult(T result) => _result = result;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        httpContext.Response.StatusCode = StatusCodes.Status200OK;

        return httpContext.Response.WriteAsJsonAsync(Envelope.Ok(_result));
    }
}
