using Microsoft.AspNetCore.Mvc;
using PetDS.Application.Positions.PositionCreate;
using PetDS.Contract;
using PetDS.Web.Response;

namespace PetDS.Web.Controllers
{
    [ApiController]
    [Route("/api/positions")]
    public class PositionController : ControllerBase
    {
        [HttpPost]
        public async Task<EdponintResult<Guid>> Create(
            [FromBody] PositionCreateDto dto,
            [FromServices] PositionCreateServise positionCreateServise)
        {
            var command = new PositionCreateCommand(dto);
            return await positionCreateServise.Handel(command);
        }
    }
}
