using CSharpFunctionalExtensions;
using PetDS.Domain.Departament;
using PetDS.Domain.Position.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Position;

public class Position : Shered.Entity<PositionId>
{
    private readonly List<DepartamentPosition> _departamentPositions;

    private Position(PositionId id) : base(id)
    {
    }

    private Position(PositionId id, PositionName name, PositionDiscription discription,
        List<Departament.Departament> departament) : base(id)
    {
        Name = name;
        Discription = discription;
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
        List<DepartamentPosition> departamentPositions =
            departament.Select(q => DepartamentPosition.Create(q, id).Value).ToList();
        _departamentPositions = departamentPositions;
    }

    public PositionName Name { get; private set; }

    public PositionDiscription? Discription { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }
    public IReadOnlyList<DepartamentPosition> departamentPositions => _departamentPositions;

    public static Result<Position, Error> Create(PositionName name, PositionDiscription discription,
        List<Departament.Departament> departament)
    {
        PositionId id = PositionId.NewGuidPosition();
        return new Position(id, name, discription, departament);
    }
}