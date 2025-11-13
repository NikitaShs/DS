using PetDS.Application.abcstractions;

namespace PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;

public record UpdateDepartamentHierarchyCommand(Guid departanetId) : ICommand;