using FluentValidation;
using PetDS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.UpdateDepartament
{
    public class UpdateDepartamentLocationsValidator : AbstractValidator<UpdateDepartamentLocationsDto>
    {
        public UpdateDepartamentLocationsValidator()
        {
            RuleFor(q => q.locationsId).NotEmpty().Must(guids => guids.Distinct().Count() == guids.Count());
        }
    }
}
