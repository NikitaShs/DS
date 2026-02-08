using Core.Adstract;
using CSharpFunctionalExtensions;
using FileService.Core.abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Features
{
    public sealed class DownloadFile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("files/{fileId:guid}", async (
                [FromRoute] Guid id,
                [FromServices] DownloadFileHandler downloadFileHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await downloadFileHandler.Handler(id, cancellationToken);

                if (res.IsFailure)
                {
                    return Results.BadRequest(res.Error);
                }

                return Results.Ok(res.Value);
            });
        }
    }

    public class DownloadFileHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<DownloadFileHandler> _logger;
        private readonly IS3Provider _s3Provider;

        public DownloadFileHandler(IMediaRepository mediaRepository, ILogger<DownloadFileHandler> logger, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
        }

        public async Task<Result<string, Errors>> Handler(Guid id, CancellationToken cancellationToken)
        {
            if(id == Guid.Empty)
            {
                _logger.LogInformation("id не указан");
                return GeneralErrors.ValueNotValid("id null").ToErrors();
            }

            var mediaAsset = await _mediaRepository.GetBy(q => q.Id == id, cancellationToken);
            if (mediaAsset.IsFailure)
            {
                _logger.LogInformation("нету файла с id {idFile}", id);
                return GeneralErrors.ValueNotFound(id).ToErrors();
            }

            var res = await _s3Provider.GenerateDownloadUrlAsync(mediaAsset.Value.StorageKey, cancellationToken);
            if (res.IsFailure)
            {
                _logger.LogInformation("неудалось сгенерировать ссылку на файл из s3 с id {idFile}", id);
                return Error.NotFound("GenerateDownloadUrlAsync.IsFailure.S3", "ошибка генерации ссылки на файл из S3").ToErrors();
            }

            return res.Value;
        }
    }
}
