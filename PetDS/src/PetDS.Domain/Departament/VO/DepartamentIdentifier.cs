﻿using CSharpFunctionalExtensions;
using PetDS.Domain.Shered;
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
        private DepartamentIdentifier()
        {
        }

        private DepartamentIdentifier(string valuseIdentifier) {
            ValueIdentifier = valuseIdentifier;
        }

        public string ValueIdentifier { get; }

        public static Result<DepartamentIdentifier, Error> Create(string valueIdentifier)
        {
            if (valueIdentifier.Length < 3 || valueIdentifier.Length > Constans.MAX_150_lENGHT_DEP || string.IsNullOrWhiteSpace(valueIdentifier) || !Regex.IsMatch(valueIdentifier, @"^[a-zA-Z]+$"))
            {
                return GeneralErrors.ValueNotValid("Identifier");
            }

            return new DepartamentIdentifier(valueIdentifier);
        }

    }
}
