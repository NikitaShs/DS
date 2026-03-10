namespace PetDS.Contract.Departamen.Queries;

public record DepartamenthAndChildModel
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Identifier { get; init; } = string.Empty;

    public Guid? ParentId { get; init; }

    public short Depth { get; init; }

    public string Path { get; init; } = string.Empty;

    public bool IsActive { get; init; }

    public bool HasMoreChildren { get; init; }

    public List<DepartamenthModelClear> Childs { get; init; }
}