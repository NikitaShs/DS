using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Departament.VO
{
    public record DepartamentPash
    {

        private DepartamentPash(string valuePash)
        {
            ValuePash = valuePash;
        }

        public string ValuePash { get; }

        public static Result<DepartamentPash> Create(string departamentName, Departament? parent)
        {

            if(parent != null)
            {
                string valuePash = $"{parent.Name}/{departamentName}";
                return new DepartamentPash(valuePash);
            }
            else
            {
                string valuePash = $"{departamentName}";
                return new DepartamentPash(valuePash);
            }
        }

    }
}
