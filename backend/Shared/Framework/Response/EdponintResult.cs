using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Response;

public class EdponintResult<TValue> : Microsoft.AspNetCore.Http.IResult, IEndpointMetadataProvider
{
    private readonly Microsoft.AspNetCore.Http.IResult _result;

    public EdponintResult(Result<TValue, Error> result) => _result =
        result.IsSuccess ? new SuccessResult<TValue>(result.Value) : new ErrorResult(result.Error);

    public EdponintResult(Result<TValue, Errors> result) => _result =
        result.IsSuccess ? new SuccessResult<TValue>(result.Value) : new ErrorResult(result.Error);

    public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
    {
    }

    public Task ExecuteAsync(HttpContext httpContext) =>
        _result.ExecuteAsync(httpContext);

    public static implicit operator EdponintResult<TValue>(Result<TValue, Error> result) => new(result);

    public static implicit operator EdponintResult<TValue>(Result<TValue, Errors> result) => new(result);
}
