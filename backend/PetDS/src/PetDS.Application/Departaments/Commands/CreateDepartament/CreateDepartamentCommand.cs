using Core.Adstract;
using PetDS.Contract;

namespace PetDS.Application.Departaments.CreateDepartament;

public record CreateDepartamentCommand(CreateDepartamentDto departamentDto, CancellationToken cancellationToken)
    : ICommand;