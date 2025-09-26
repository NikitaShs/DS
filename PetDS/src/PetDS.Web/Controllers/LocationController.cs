using Microsoft.AspNetCore.Mvc;
using PetDS.Application;
using PetDS.Contract;
using CSharpFunctionalExtensions;

namespace PetDS.Web.Controllers
{
[ApiController]

[Route("/api/locations")]

    public class LocationController : ControllerBase
    {

        [HttpPost]
        public async Task<Result<Guid>> CreateLocation(
            [FromServices] LocationService locationService,
            [FromBody] CreateLocationRequest createLocation,
            CancellationToken cancellationToken)
        {
            return await locationService.CreateLoc(createLocation, cancellationToken);

        }
    }
}
