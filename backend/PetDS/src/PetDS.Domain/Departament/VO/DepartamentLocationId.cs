namespace PetDS.Domain.Departament.VO;

public record DepartamentLocationId
{
    private DepartamentLocationId() { }
    private DepartamentLocationId(Guid valueId) => ValueId = valueId;

    public Guid ValueId { get; }

    public static DepartamentLocationId CreateNewGuid() => new(Guid.NewGuid());

    public static DepartamentLocationId CreateEmpty() => new(Guid.Empty);

    public static DepartamentLocationId Create(Guid valueId) => new(valueId);
}