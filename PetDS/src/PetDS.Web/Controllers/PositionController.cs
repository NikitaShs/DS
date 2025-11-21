using Microsoft.AspNetCore.Mvc;
using PetDS.Application.Positions.PositionCreate;
using PetDS.Contract.Departamen;
using PetDS.Web.Response;

namespace PetDS.Web.Controllers;

[ApiController]
[Route("/api/positions")]
public class PositionController : ControllerBase
{
    [HttpPost]
    public async Task<EdponintResult<Guid>> Create(
        [FromBody] PositionCreateDto dto,
        [FromServices] PositionCreateServise positionCreateServise)
    {
        PositionCreateCommand command = new(dto);
        return await positionCreateServise.Handler(command);
    }
}