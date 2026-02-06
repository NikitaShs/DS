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
            app.MapDelete("DownloadFile/{id}", async(
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
        private readonly ILogger<DownloadFile> _logger;
        private readonly IS3Provider _s3Provider;

        public DownloadFileHandler(IMediaRepository mediaRepository, ILogger<DownloadFile> logger, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
        }

        public async Task<Result<string, Error>> Handler(Guid id, CancellationToken cancellationToken)
        {
            if(id == Guid.Empty)
            {
                _logger.LogInformation("id не указан");
                GeneralErrors.ValueNotValid("id null");
            }

            var mediaAsset = _mediaRepository.GetFileByIdAsync(id, cancellationToken);
            if (mediaAsset.Result.IsFailure)
            {
                _logger.LogInformation("нету файла с id {idFile}", id);
                GeneralErrors.ValueNotFound(id);
            }

            var res = await _s3Provider.GenerateDownloadUrlAsync(mediaAsset.Result.Value.StorageKey, cancellationToken);
            if (res.IsFailure)
            {
                _logger.LogInformation("неудалось сгенерировать ссылку на файл из s3 с id {idFile}", id);
                Error.NotFound("GenerateDownloadUrlAsync.IsFailure.S3", "ошибка генерации ссылки на файл из S3");
            }

            return res;
        }
    }
}
