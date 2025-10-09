using FluentValidation;
using PetDS.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Locations.CreateLocation
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
    {
        public CreateLocationValidator()
        {
            RuleFor(q => q.city).NotEmpty();
            RuleFor(q => q.namberHouse).NotEmpty();
            RuleFor(q => q.strit).NotEmpty();
            RuleFor(q => q.name).Length(3, 120).NotEmpty();
            RuleFor(q => q.region).NotEmpty();

        }
    }
}
