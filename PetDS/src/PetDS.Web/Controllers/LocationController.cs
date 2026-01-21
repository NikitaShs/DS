using Core.Adstract;
using Framework.Response;
using Microsoft.AspNetCore.Mvc;
using PetDS.Application.Locations.Commands.CreateLocation;
using PetDS.Application.Locations.Queries;
using PetDS.Contract;
using PetDS.Contract.Location.Queries;

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
        CreateLocationCommand command = new(request);
        return await handler.Handler(command, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<GetLocationDto>> GetLocation(
        [FromServices] GetLocationFullServise handler,
        [FromQuery] GetLocationFullRequestDto request,
        CancellationToken cancellationToken) =>
        Ok(await handler.Handler(request, cancellationToken));
}