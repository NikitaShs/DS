using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetDS.Domain.Departament.VO
{
    public record DepartamentIdentifier
    {
        private DepartamentIdentifier(string valuseIdentifier) {
            ValueIdentifier = valuseIdentifier;
        }

        public string ValueIdentifier { get; }

        public static Result<DepartamentIdentifier> Create(string valueIdentifier)
        {
            if (valueIdentifier.Length < 3 || valueIdentifier.Length > 150 || string.IsNullOrWhiteSpace(valueIdentifier) || !Regex.IsMatch(valueIdentifier, @"^[a-zA-Z]+$"))
            {
                return Result.Failure<DepartamentIdentifier>("Не валидный аутентификатор");
            }

            return new DepartamentIdentifier(valueIdentifier);
        }

    }
}
