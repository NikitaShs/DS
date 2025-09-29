using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Position.VO;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Departament
{
    public class DepartamentPosition : Shered.Entity<DepartamentPositionId>
    {
        private DepartamentPosition(DepartamentPositionId id) : base(id)
        {
        }

        private DepartamentPosition(DepartamentPositionId departamentPositionId, Departament departament, PositionId positionId) : base(departamentPositionId)
        {
            Departament = departament;
            DepartamentId = departament.Id;
            PositionId = positionId;
        }

        public DepartamentId DepartamentId { get; private set; }

        public Departament Departament { get; private set; }

        public PositionId PositionId { get; private set; }

        public static Result<DepartamentPosition, Error> Create(Departament departament, PositionId positionId)
        {
            var id = DepartamentPositionId.CreateNewGuid();
            return new DepartamentPosition(id, departament, positionId);
        }
    }

}