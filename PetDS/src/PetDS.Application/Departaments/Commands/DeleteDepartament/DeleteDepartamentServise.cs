using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetDS.Application.abcstractions;
using PetDS.Domain.Departament.VO;
using PetDS.Domain.Shered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.Commands.DeleteDepartament
{
    public class DeleteDepartamentServise : IHandler<Guid, DeleteDepartamentCommand>
    {
        private readonly ILogger<DeleteDepartamentServise> _logger;
        private readonly IDepartamentRepository _departamentRepository;
        private readonly IConnectionManeger _connectionManeger;

        public DeleteDepartamentServise(ILogger<DeleteDepartamentServise> logger, IDepartamentRepository departamentRepository, IConnectionManeger connectionManeger)
        {
            _logger = logger;
            _departamentRepository = departamentRepository;
            _connectionManeger = connectionManeger;
        }

        public async Task<Result<Guid, Errors>> Handler(DeleteDepartamentCommand command, CancellationToken cancellationToken)
        {

            if (!_departamentRepository.CheckingDepartamentExistence(DepartamentId.Create(command.departamenId), cancellationToken).Result.Value)
            {
                _logger.LogInformation("департамент не существует");
                return GeneralErrors.ValueNotValid("departamenId").ToErrors();
            }

            using var tran = _connectionManeger.CreateTranzit(cancellationToken).Result.Value;

            tran.Commit();


            var res = await _departamentRepository.SoftDeleteDept(command.departamenId, cancellationToken);

            if (res.IsFailure)
                return GeneralErrors.Update("SoftDeleteDept").ToErrors();
            else
                return command.departamenId;
        }
    }
}
