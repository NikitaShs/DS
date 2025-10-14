using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Application.Departaments;
using PetDS.Contract;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Position;
using PetDS.Domain.Position.VO;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Positions.PositionCreate
{
    public class PositionCreateServise : IHandler<Guid, PositionCreateCommand>
    {
        private readonly ILogger<PositionCreateServise> _logger;
        private readonly IPositionRepositiry _positionRepositiry;
        private readonly IValidator<PositionCreateDto> _validator;
        private readonly IDepartamentRepository _departamentRepository;


        public PositionCreateServise(ILogger<PositionCreateServise> logger, IPositionRepositiry positionRepositiry, IValidator<PositionCreateDto> validator,
            IDepartamentRepository departamentRepository)
        {
            _logger = logger;
            _positionRepositiry = positionRepositiry;
            _validator = validator;
            _departamentRepository = departamentRepository;
        }

        public async Task<Result<Guid, Errors>> Handel(PositionCreateCommand command, CancellationToken cancellationToken = default)
        {

            var result = _validator.Validate(command.dto);

            if (!result.IsValid)
            {
                _logger.LogInformation("не валидное джто");
                return GeneralErrors.ValueNotValid("dto").ToErrors();
            }

            var name = PositionName.Create(command.dto.name);
            if (name.IsFailure)
            {
                _logger.LogInformation("имя не создано");
                return GeneralErrors.ValueFailure("name").ToErrors();
            }



            var discription = command.dto.discription != null ? Domain.Position.VO.Position.Create(command.dto.discription) : Domain.Position.VO.Position.Create(string.Empty);

            if (discription.IsFailure)
            {
                _logger.LogInformation("описание не созданно");
                return GeneralErrors.ValueFailure("discription").ToErrors();
            }

            var departamentId = command.dto.departamentId.Select(q => DepartamentId.Create(q)).ToList();

            var departaments = await _departamentRepository.GetDepartamentsById(departamentId, cancellationToken);

            if (departaments.IsFailure)
            {
                _logger.LogInformation("не все DepartamentId существуют или активны");
                return GeneralErrors.Unknown().ToErrors();
            }


            var position = Domain.Position.Position.Create(name.Value, discription.Value, departaments.Value);
            if (position.IsFailure)
            {
                _logger.LogInformation("positions не созданно");
                return GeneralErrors.ValueFailure("position").ToErrors();
            }


            var resultFinal = await _positionRepositiry.AddPosition(position.Value, cancellationToken);

            return resultFinal.Value;
        }
    }
}
