using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.Metadata;
using PetDS.Domain.Shered;
using System.Reflection;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace PetDS.Web.Response
{
    public class EdponintResult<TValue> : IResult, IEndpointMetadataProvider
    {
        private readonly IResult _result;

        public EdponintResult(Result<TValue, Error> result)
        {
            _result = result.IsSuccess ? new SuccessResult<TValue>(result.Value) : new ErrorResult(result.Error);
        }

        public static implicit operator EdponintResult<TValue>(Result<TValue, Error> result) => new(result);

        public static void PopulateMetadata(MethodInfo method, EndpointBuilder builder)
        {

        }

        public Task ExecuteAsync(HttpContext httpContext) =>
            _result.ExecuteAsync(httpContext);

    }
}
