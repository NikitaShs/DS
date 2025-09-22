using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Departament
{
    public class DepartamentPosition : Shered.Entity<DepartamentPositionId>
    {
        private DepartamentPosition(DepartamentPositionId departamentPositionId, Departament departament, Guid positionId) : base(departamentPositionId)
        {
            Departament = departament;
            PositionId = positionId;
        }

        public Departament Departament { get; private set; }

        public Guid PositionId { get; private set; }

        public static Result<DepartamentPosition> Create(Departament departament, Guid positionId)
        {
            var id = DepartamentPositionId.CreateNewGuid();
            return new DepartamentPosition(id, departament, positionId);
        }
    }

}