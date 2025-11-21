namespace PetDS.Contract.Departamen;

public record PositionCreateDto(string name, List<Guid> departamentId, string discription = null);