using CSharpFunctionalExtensions;
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

        private Position(PositionId id, PositionName name, PositionDiscription discription) : base(id)
        {
            Name = name;
            Discription = discription;
            IsActive = true;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public PositionName Name { get; private set; }

        public PositionDiscription? Discription { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateAt { get; private set; }

        public DateTime UpdateAt { get; private set; }

        public static Result<Position, Error> Create(PositionName name, PositionDiscription discription)
        {
            var id = PositionId.NewGuidPosition();
            return new Position(id, name, discription);
        }

    }

}