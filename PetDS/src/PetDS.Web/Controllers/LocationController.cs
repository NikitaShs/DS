using Microsoft.AspNetCore.Mvc;
using PetDS.Application.abcstractions;
using PetDS.Application.Locations.Commands.CreateLocation;
using PetDS.Contract;
using PetDS.Web.Response;

namespace PetDS.Web.Controllers;

[ApiController]
[Route("/api/locations")]
public class LocationController : ControllerBase
{
    [HttpPost]
    public async Task<EdponintResult<Guid>> CreateLocation(
        [FromServices] IHandler<Guid, CreateLocationCommand> handler,
        [FromBody] CreateLocationDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateLocationCommand(request);
        return await handler.Handler(command, cancellationToken);
    }
}