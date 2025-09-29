﻿using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;
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

        public static Result<DepartamentName, Error> Create(string valueName)
        {
            if(valueName.Length < 3 || valueName.Length > Constans.MAX_150_lENGHT_DEP || string.IsNullOrWhiteSpace(valueName))
            {
                return GeneralErrors.ValueNotValid("Name");
            }

            return new DepartamentName(valueName);
        }
    }
}
