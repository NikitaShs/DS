using Microsoft.AspNetCore.Routing;

namespace FileService.Core.abstractions
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }

}