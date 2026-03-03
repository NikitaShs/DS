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
    public sealed class AttachmentFileToDepartamentHandler
    {
        private readonly ILogger<AttachmentFileToDepartamentHandler> _logger;
        private readonly IDepartamentRepository _departamentRepository;

        public AttachmentFileToDepartamentHandler(ILogger<AttachmentFileToDepartamentHandler> logger, IDepartamentRepository departamentRepository)
        {
            _logger = logger;
            _departamentRepository = departamentRepository;
        }

        public async Task Handle(VideoCreate message, CancellationToken cancellationToken)
        {
            var dept = await _departamentRepository.GetDepartamentById(DepartamentId.Create(message.AssetsType), cancellationToken);
            if (dept.IsFailure)
            {
                _logger.LogInformation("департамент с id {id} не найден", message.AssetsType);
            }

            dept.Value.AttachmentFile(message.VideoId);
            await _departamentRepository.SaveAsync(cancellationToken);

        }
    }
}
