using Core.MessagingComminication.MessagingDto;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using PetDS.Application.Departaments;
using PetDS.Domain.Departament.VO;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Application.Departaments.IntegrationEvents
{
    public sealed class AttachmentFileToDepartament
    {
        private readonly ILogger<AttachmentFileToDepartament> _logger;
        private readonly IDepartamentRepository _departamentRepository;

        public AttachmentFileToDepartament(ILogger<AttachmentFileToDepartament> logger, IDepartamentRepository departamentRepository)
        {
            _logger = logger;
            _departamentRepository = departamentRepository;
        }

        public async Task Handler(VideoCreate massage, CancellationToken cancellationToken)
        {
            var dept = await _departamentRepository.GetDepartamentById(DepartamentId.Create(massage.AssetsType), cancellationToken);
            if (dept.IsFailure)
            {
                _logger.LogInformation("департамент с id {id} не найден", massage.AssetsType);
            }

            dept.Value.AttachmentFile(massage.VideoId);
            await _departamentRepository.SaveAsync(cancellationToken);

        }
    }
}
