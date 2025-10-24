using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Locations;
using PetDS.Contract;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Location.VO;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.UpdateDepartament
{
    public class UpdateDepartamentLocationsServise : IHandler<Guid, UpdateDepartamentLocationsCommand>
    {
        private readonly IValidator<UpdateDepartamentLocationsDto> _validator;
        private readonly ILogger<UpdateDepartamentLocationsServise> _logger;
        private readonly IDepartamentRepository _departamentRepository;
        private readonly ILocationRepository _locationRepository;

        public UpdateDepartamentLocationsServise(
            IValidator<UpdateDepartamentLocationsDto> validator,
            ILogger<UpdateDepartamentLocationsServise> logger,
            IDepartamentRepository departamentRepository,
            ILocationRepository locationRepository)
        {
            _validator = validator;
            _logger = logger;
            _departamentRepository = departamentRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Result<Guid, Errors>> Handler(UpdateDepartamentLocationsCommand command, CancellationToken cancellationToken = default)
        {
            var result = _validator.ValidateAsync(command.dto, cancellationToken);
            if (result.IsFaulted)
            {
                return GeneralErrors.ValueNotValid("locations").ToErrors();
            }

            var locId = command.dto.locationsId.Select(q => LocationId.Create(q)).ToList();

            if (!_locationRepository.ChekActivetiLocations(locId, cancellationToken).Result.Value)
            {
                return GeneralErrors.ValueNotValid("logic").ToErrors();
            }

            var departamentId = DepartamentId.Create(command.departamentId);

            await _departamentRepository.UpdateLocations(locId, departamentId, cancellationToken);

            return departamentId.ValueId;
        }
    }
}