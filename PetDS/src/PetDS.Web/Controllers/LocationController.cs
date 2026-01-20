using Core.adstract;
using Framework.Response;
using Microsoft.AspNetCore.Mvc;
using PetDS.Application.abcstractions;
using PetDS.Application.Locations.Commands.CreateLocation;
using PetDS.Application.Locations.Queries;
using PetDS.Contract;
using PetDS.Contract.Departamen.Queries;
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
        var command = new CreateLocationCommand(request);
        return await handler.Handler(command, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<GetLocationDto>> GetLocation(
        [FromServices] GetLocationFullServise handler,
        [FromQuery] GetLocationFullRequestDto request,
        CancellationToken cancellationToken)
    {
        return Ok(await handler.Handler(request, cancellationToken));
    }
}