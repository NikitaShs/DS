using Microsoft.AspNetCore.Mvc;
using PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;
using PetDS.Application.Departaments.CreateDepartament;
using PetDS.Application.Departaments.Queries;
using PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;
using PetDS.Contract;
using PetDS.Contract.Departamen;
using PetDS.Contract.Departamen.Queries;
using PetDS.Web.Response;

namespace PetDS.Web.Controllers;

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
        UpdateDepartamentLocationsCommand command = new(dto, departamenId);

        return await updateDepartamentLocationsServise.Handler(command, cancellationToken);
    }

    [HttpPatch("/{departamenId}Hierarhy")]
    public async Task<EdponintResult<Guid>> UpdateDepartamentHierarhy(
        [FromBody] UpdateDepartamentHierarchyDto dto,
        [FromServices] UpdateDepartamentHierarchyServise servise,
        [FromRoute] Guid departamenId)
    {
        var command = new UpdateDepartamentHierarchyCommand(departamenId, dto);
        return await servise.Handler(command);
    }

}