using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Departament;

public class DepartamentLocation : Shered.Entity<DepartamentLocationId>
{
    private DepartamentLocation(DepartamentLocationId id) : base(id)
    {
    }

    private DepartamentLocation(DepartamentLocationId departamentLocationId, Departament departament,
        LocationId locationId) : base(departamentLocationId)
    {
        Departament = departament;
        DepartamentId = departament.Id;
        LocationId = locationId;
    }

    public DepartamentId DepartamentId { get; private set; }

    public Departament Departament { get; private set; }

    public LocationId LocationId { get; private set; }

    public static Result<DepartamentLocation, Error> Create(Departament departament, LocationId locationId)
    {
        DepartamentLocationId id = DepartamentLocationId.CreateNewGuid();

        return new DepartamentLocation(id, departament, locationId);
    }
}