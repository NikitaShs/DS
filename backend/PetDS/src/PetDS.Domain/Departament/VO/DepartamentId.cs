namespace PetDS.Domain.Departament.VO;

public record DepartamentId
{
    private DepartamentId()
    {
    }

    private DepartamentId(Guid valueId) => ValueId = valueId;

    public Guid ValueId { get; }

    public static DepartamentId CreateNewGuid() => new(Guid.NewGuid());

    public static DepartamentId CreateEmpty() => new(Guid.Empty);

    public static DepartamentId Create(Guid valueId) => new(valueId);
}