using Core.Adstract;
using CSharpFunctionalExtensions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using PetDS.Application.Departaments;
using PetDS.Contract.Departamen;
using PetDS.Domain.Departament;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Position;
using PetDS.Domain.Position.VO;
using SharedKernel.Exseption;

namespace PetDS.Application.Positions.PositionCreate;

public class PositionCreateServise : IHandler<Guid, PositionCreateCommand>
{
    private readonly IDepartamentRepository _departamentRepository;
    private readonly ILogger<PositionCreateServise> _logger;
    private readonly IPositionRepositiry _positionRepositiry;
    private readonly IValidator<PositionCreateDto> _validator;


    public PositionCreateServise(ILogger<PositionCreateServise> logger, IPositionRepositiry positionRepositiry,
        IValidator<PositionCreateDto> validator,
        IDepartamentRepository departamentRepository)
    {
        _logger = logger;
        _positionRepositiry = positionRepositiry;
        _validator = validator;
        _departamentRepository = departamentRepository;
    }

    public async Task<Result<Guid, Errors>> Handler(PositionCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        ValidationResult? result = _validator.Validate(command.dto);

        if (!result.IsValid)
        {
            _logger.LogInformation("не валидное джто");
            return GeneralErrors.ValueNotValid("dto").ToErrors();
        }

        Result<PositionName, Error> name = PositionName.Create(command.dto.name);
        if (name.IsFailure)
        {
            _logger.LogInformation("имя не создано");
            return GeneralErrors.ValueFailure("name").ToErrors();
        }


        Result<PositionDiscription, Error> discription = command.dto.discription != null
            ? PositionDiscription.Create(command.dto.discription)
            : PositionDiscription.Create(string.Empty);

        if (discription.IsFailure)
        {
            _logger.LogInformation("описание не созданно");
            return GeneralErrors.ValueFailure("discription").ToErrors();
        }

        List<DepartamentId> departamentId = command.dto.departamentId.Select(q => DepartamentId.Create(q)).ToList();

        Result<List<Departament>, Errors> departaments =
            await _departamentRepository.GetDepartamentsById(departamentId, cancellationToken);

        if (departaments.IsFailure)
        {
            _logger.LogInformation("не все DepartamentId существуют или активны");
            return GeneralErrors.Unknown().ToErrors();
        }


        Result<Position, Error> position = Position.Create(name.Value, discription.Value, departaments.Value);
        if (position.IsFailure)
        {
            _logger.LogInformation("positions не созданно");
            return GeneralErrors.ValueFailure("position").ToErrors();
        }


        Result<Guid, Errors> resultFinal = await _positionRepositiry.AddPosition(position.Value, cancellationToken);

        return resultFinal.Value;
    }
}