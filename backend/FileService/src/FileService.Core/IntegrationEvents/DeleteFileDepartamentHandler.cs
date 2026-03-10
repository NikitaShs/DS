using Amazon.Runtime.Internal.Util;
using Core.MessagingComminication.MessagingDto;
using FileService.Core.abstractions;
using FileService.Core.Features;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.IntegrationEvents
{
    public class DeleteFileDepartamentHandler
    {
        private readonly ILogger<DeleteFileDepartamentHandler> _logger;
        private readonly IMediaRepository _mediaRepository;
        private readonly DeleteFileHandler _deleteFileHandler;

        public DeleteFileDepartamentHandler(ILogger<DeleteFileDepartamentHandler> logger, IMediaRepository mediaRepository, DeleteFileHandler deleteFileHandler)
        {
            _logger = logger;
            _mediaRepository = mediaRepository;
            _deleteFileHandler = deleteFileHandler;
        }

        public async Task Handle(DepartamentDelete message, CancellationToken cancellationToken)
        {

            var mediaAsset = await _mediaRepository.GetBy(q => q.Owner.EntiteId == message.departamentId, cancellationToken);

            if (mediaAsset.IsFailure)
                _logger.LogInformation("неудалось найти файл привязанный к данному депактаменту");

            await _deleteFileHandler.Handler(mediaAsset.Value.Id, cancellationToken);
        }
    }
}
