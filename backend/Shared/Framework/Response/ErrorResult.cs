using Microsoft.AspNetCore.Http;
using SharedKernel.Exseption;

namespace Framework.Response;

public class ErrorResult : IResult
{
    private readonly Errors _errors;

    public ErrorResult(Errors errors) => _errors = errors;

    public Task ExecuteAsync(HttpContext httpContext)
    {
        if (_errors == null)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return httpContext.Response.WriteAsJsonAsync(Envelope.Error(GeneralErrors.Unknown()));
        }

        List<ErrorType> typeError = _errors.Select(e => e.Type).Distinct().ToList();

        int statusCode = typeError.Count() > 1
            ? StatusCodes.Status500InternalServerError
            : GetStatusError(typeError.First());

        httpContext.Response.StatusCode = statusCode;

        return httpContext.Response.WriteAsJsonAsync(Envelope.Error(_errors));
    }

    private static int GetStatusError(ErrorType error) => error switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Failure => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status500InternalServerError
    };
}