namespace PetDS.Domain.Departament.VO;

public record DepartamentPositionId
{
    private DepartamentPositionId() { }
    private DepartamentPositionId(Guid valueId) => ValueId = valueId;

    public Guid ValueId { get; }

    public static DepartamentPositionId CreateNewGuid() => new(Guid.NewGuid());

    public static DepartamentPositionId CreateEmpty() => new(Guid.Empty);

    public static DepartamentPositionId Create(Guid valueId) => new(valueId);
}