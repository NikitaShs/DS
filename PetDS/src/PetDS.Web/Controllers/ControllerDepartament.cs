using Microsoft.AspNetCore.Mvc;
using PetDS.Application.Departaments.CreateDepartament;
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

            return await departamentCreate.Handel(command, cancellationToken);
        }
    }
}
