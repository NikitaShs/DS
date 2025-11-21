using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Departament;

public class Departament : Shered.Entity<DepartamentId>
{
    private readonly List<Departament> _childrenDepartament = [];

    private readonly List<DepartamentLocation> _departamentLocation = [];

    private readonly List<DepartamentPosition> _departamentPosition = [];

    private Departament(DepartamentId id) : base(id)
    {
    }

    public Departament(DepartamentId id, DepartamentName name,
        DepartamentIdentifier identifier, Departament parent,
        DepartamentPash path, IEnumerable<LocationId> locationId,
        short depth) : base(id)
    {
        Name = name;
        Identifier = identifier;
        Parent = parent;
        ParentId = parent?.Id;
        Path = path;
        Depth = depth;
        IEnumerable<DepartamentLocation> depLoc = locationId.Select(loc => DepartamentLocation.Create(this, loc).Value);
        _departamentLocation = depLoc.ToList();
        IsActive = true;
        CreateAt = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
    }

    public DepartamentName Name { get; private set; }

    public DepartamentIdentifier Identifier { get; private set; }

    public Departament? Parent { get; private set; }

    public DepartamentId? ParentId { get; private set; }

    public DepartamentPash Path { get; private set; }

    public short Depth { get; }

    public bool IsActive { get; private set; }

    public DateTime CreateAt { get; private set; }

    public DateTime UpdateAt { get; private set; }

    public Guid LocationId { get; private set; }

    public IReadOnlyCollection<Departament> Children => _childrenDepartament;

    public IReadOnlyCollection<DepartamentLocation> DepartamentLocations => _departamentLocation;

    public IReadOnlyCollection<DepartamentPosition> DepartamentPositions => _departamentPosition;


    // CSharpFunctionalExtensions
    public static Result<Departament, Error> Create(
        DepartamentName name,
        DepartamentIdentifier identifier,
        Departament? parent, IEnumerable<LocationId> locationId)
    {
        DepartamentId id = DepartamentId.CreateNewGuid();

        Result<DepartamentPash, Error> path = DepartamentPash.Create(name.ValueName, parent);

        if (path.IsFailure)
        {
            return GeneralErrors.ValueFailure("path");
        }

        short depth = 1;

        if (parent == null)
        {
            depth = 1;
        }
        else
        {
            depth = (short)(parent.Depth + 1);
        }

        return new Departament(id, name, identifier, parent, path.Value, locationId, depth);
    }
}