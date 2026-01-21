namespace PetDS.Contract.Departamen.Queries;

public record DepartamentModelDto
{
    public string Name { get; init; } = string.Empty;

    public string Identifier { get; init; } = string.Empty;

    public Guid? ParentId { get; init; }

    public short Depth { get; init; }

    public string Path { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public int CountPos { get; init; }
}