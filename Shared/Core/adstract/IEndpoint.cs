using Microsoft.AspNetCore.Routing;

namespace Core.Adstract
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }

}