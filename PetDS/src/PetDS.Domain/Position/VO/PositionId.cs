namespace PetDS.Domain.Position.VO;

public record PositionId
{
    private PositionId() { }
    private PositionId(Guid vakueId) => ValueId = vakueId;

    public Guid ValueId { get; }

    public static PositionId NewGuidPosition() => new(Guid.NewGuid());

    public static PositionId EmptyId() => new(Guid.Empty);

    public static PositionId Create(Guid valueId) => new(valueId);
}