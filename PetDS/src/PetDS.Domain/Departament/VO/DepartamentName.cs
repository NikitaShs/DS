using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Domain.Departament.VO
{
    public record DepartamentName
    {
        private DepartamentName(string valueName)
        {
            ValueName = valueName;
        }

        public string ValueName { get; }

        public static Result<DepartamentName> Create(string valueName)
        {
            if(valueName.Length < 3 || valueName.Length > 150 || string.IsNullOrWhiteSpace(valueName))
            {
                return Result.Failure<DepartamentName>("Не валидное имя");
            }

            return new DepartamentName(valueName);
        }
    }
}
