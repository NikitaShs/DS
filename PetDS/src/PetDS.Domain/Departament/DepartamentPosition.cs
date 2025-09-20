using PetDS.Domain.Departament.VO;

namespace PetDS.Domain.Departament
{
    public class DepartamentPosition
    {
        public DepartamentPositionId DepartamentPositionId { get; private set; }

        public DepartamentId DepartamentId { get; private set; }

        public Guid PositionId { get; private set; }
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
