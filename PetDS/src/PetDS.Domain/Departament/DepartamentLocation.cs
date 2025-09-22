using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Departament
{
    public class DepartamentLocation : Shered.Entity<DepartamentLocationId>
    {
        private DepartamentLocation(DepartamentLocationId departamentLocationId, Departament departament, Guid locationId) : base(departamentLocationId)
        {
            Departament = departament;
            LocationId = locationId;
        }

        public Departament Departament { get; private set; }

        public Guid LocationId { get; private set; }

        public static Result<DepartamentLocation> Create(Departament departament, Guid locationId)
        {
            var id = DepartamentLocationId.CreateNewGuid();

            return new DepartamentLocation(id, departament, locationId);
        }

    }

}