using CSharpFunctionalExtensions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetDS.Domain.Departament
{
    public class Departament : Shered.Entity<DepartamentId>
    {
        public Departament(DepartamentId id, DepartamentName name, DepartamentIdentifier identifier, Departament parent, DepartamentPash path, short depth) : base(id)
        {
            Name = name;
            Identifier = identifier;
            Parent = parent;
            Path = path;
            Depth = depth;
            IsActive = true;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public DepartamentName Name { get; private set; }

        public DepartamentIdentifier Identifier { get; private set; }

        public Departament? Parent { get; private set; }

        public DepartamentPash Path { get; private set; }

        public short Depth { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreateAt { get; private set; }

        public DateTime UpdateAt { get; private set; }

        private List<Departament> _childrenDepartament = [];

        private List<DepartamentLocation> _departamentLocation = [];

        private List<DepartamentPosition> _departamentPosition = [];

        public IReadOnlyCollection<Departament> Children => _childrenDepartament;

        public IReadOnlyCollection<DepartamentLocation> DepartamentLocation => _departamentLocation;

        public IReadOnlyCollection<DepartamentPosition> DepartamentPosition => _departamentPosition;




        // CSharpFunctionalExtensions
        public static Result<Departament> Create(
            DepartamentName name,
            DepartamentIdentifier identifier,
            Departament parent,
            )
        {
            var id = DepartamentId.CreateNewGuid();

            var path = DepartamentPash.Create(name.ValueName, parent).Value;

            short depth = 0;

            if (parent == null)
            {
                depth = 0;
            }
            else
            {
                depth = (short)(parent.Depth + 1);
            }

            var deportament = new Departament(id, name, identifier, parent, path, depth);
            return Result.Success<Departament>(deportament);
        }

    }

}













































