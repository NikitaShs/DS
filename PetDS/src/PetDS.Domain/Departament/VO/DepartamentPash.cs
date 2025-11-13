using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;

namespace PetDS.Domain.Departament.VO;

public record DepartamentPash
{
    private DepartamentPash(string valuePash) => ValuePash = valuePash;

    public string ValuePash { get; }

    public static Result<DepartamentPash, Error> Create(string departamentName, Departament? parent)
    {
        if (parent != null)
        {
            string valuePash = $"{parent.Path.ValuePash}.{departamentName}";
            return new DepartamentPash(valuePash);
        }
        else
        {
            string valuePash = $"{departamentName}";
            return new DepartamentPash(valuePash);
        }
    }
}