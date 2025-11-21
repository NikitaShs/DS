namespace PetDS.Contract;

public record CreateDepartamentDto(string name, string identifier, List<Guid> locationsId, Guid? parantId = null);