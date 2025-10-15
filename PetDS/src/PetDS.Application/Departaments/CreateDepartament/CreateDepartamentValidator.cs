using FluentValidation;
using PetDS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.CreateDepartament
{
    public class CreateDepartamentValidator : AbstractValidator<CreateDepartamentDto>
    {

        public CreateDepartamentValidator()
        {
            RuleFor(q => q.identifier).NotEmpty().Length(3, 150).Matches("^[a-zA-Z]+$");
            RuleFor(q => q.name).NotEmpty().Length(3, 150);
            RuleFor(q => q.locationsId).NotEmpty().Must(guids => guids.Distinct().Count() == guids.Count);
        }
    }
}
