using Core.Adstract;

namespace PetDS.Application.Departaments.Commands.DeleteDepartament;

public record DeleteDepartamentCommand(Guid departamenId) : ICommand;