using Microsoft.AspNetCore.Mvc;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Application.Departaments.UpdateDepartament;
using PetDS.Contract;
using PetDS.Domain.Shered;
using PetDS.Web.Response;

namespace PetDS.Web.Controllers
{

    [ApiController]

    [Route("/api/departaments")]
    public class ControllerDepartament : ControllerBase
    {
        [HttpPost]
        public async Task<EdponintResult<Guid>> CreateDepartament(
            [FromBody] CreateDepartamentDto departamentDto,
            [FromServices] DepartamentCreateServise departamentCreate,
            CancellationToken cancellationToken)
        {
            var command = new CreateDepartamentCommand(departamentDto, cancellationToken);

            return await departamentCreate.Handler(command, cancellationToken);
        }

        [HttpPatch("/{departamenId}/locations")]
        public async Task<EdponintResult<Guid>> UpdateDepartamentLocations(
            [FromServices] UpdateDepartamentLocationsServise updateDepartamentLocationsServise,
            [FromBody] UpdateDepartamentLocationsDto dto,
            [FromRoute] Guid departamenId, CancellationToken cancellationToken)
        {
            var command = new UpdateDepartamentLocationsCommand(dto, departamenId);

            return await updateDepartamentLocationsServise.Handler(command, cancellationToken);
        }
    }
}
