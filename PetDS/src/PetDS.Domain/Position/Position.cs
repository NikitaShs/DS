using CSharpFunctionalExtensions;
using PetDS.Domain.Departament;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Position.VO;
using PetDS.Domain.Shered;
using System.Xml.Linq;

namespace PetDS.Domain.Position
{
    public class Position : Shered.Entity<PositionId>
    {
        private Position(PositionId id) : base(id)
        {
        }

        private Position(PositionId id, PositionName name, VO.Position discription, List<Departament.Departament> departament) : base(id)
        {
            Name = name;
            Discription = discription;
            IsActive = true;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            var departamentPositions = departament.Select(q => DepartamentPosition.Create(q, id).Value).ToList();
            _departamentPositions = departamentPositions;
        }

        public PositionName Name { get; private set; }

        public VO.Position? Discription { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateAt { get; private set; }

        public DateTime UpdateAt { get; private set; }

        private List<DepartamentPosition> _departamentPositions;
        public IReadOnlyList<DepartamentPosition> departamentPositions => _departamentPositions;

        public static Result<Position, Error> Create(PositionName name, VO.Position discription, List<Departament.Departament> departament)
        {
            var id = PositionId.NewGuidPosition();
            return new Position(id, name, discription, departament);
        }

    }

}