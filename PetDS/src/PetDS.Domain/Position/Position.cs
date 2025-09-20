using CSharpFunctionalExtensions;
using PetDS.Domain.Position.VO;
using PetDS.Domain.Shered;
using System.Xml.Linq;

namespace PetDS.Domain.Position
{
    public class Position : Shered.Entity<PositionId>
    {
        private Position(PositionId id, PositionName name, PositionDiscription discription) : base(id)
        {
            Name = name;
            Discription = discription;
            IsActive = true;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        public PositionName Name { get; private set; }

        public PositionDiscription? Discription { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateAt { get; private set; }

        public DateTime UpdateAt { get; private set; }

        public static Result<Position> Create(PositionName name, PositionDiscription discription)
        {
            var id = PositionId.NewGuidPosition();
            return new Position(id, name, discription);
        }

    }

}













































        // CSharpFunctionalExtensions
        //public static Result<Departament> Create(string name, string identifier, Guid parentId, string path, short depth, bool isActive)
        //{
        //    if(name.Length < 3 || name.Length> 150 || string.IsNullOrWhiteSpace(name) )
        //    {
        //        return Result.Failure<Departament>("Неверно указанно имя");
        //    }

        //    if (identifier.Length < 3 || identifier.Length > 150 || string.IsNullOrWhiteSpace(identifier))
        //    {
        //        return Result.Failure<Departament>("Неверно указан identifier");
        //    }

        //    var deportament = new Departament(name, identifier, parentId, path, depth, isActive);

        //    return Result.Success<Departament>(deportament);
        //}
