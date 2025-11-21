namespace PetDS.Domain.Location.VO;

public record LocationId
{
    private LocationId() { }
    private LocationId(Guid valueId) => ValueId = valueId;

    public Guid ValueId { get; }

    public static LocationId NewGuidLocation() => new(Guid.NewGuid());
    public static LocationId EmptyLocation() => new(Guid.Empty);

    public static LocationId Create(Guid valueId) => new(valueId);
}