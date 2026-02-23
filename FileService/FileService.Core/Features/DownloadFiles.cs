using Core.Adstract;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using FileService.Core.abstractions;
using FileService.Domain.Entites;
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
    public sealed class DownloadFiles : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("files/ids", async (
                [FromQuery] Guid[] fileIds,
                [FromServices] DownloadFilesHandler downloadFilesHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await downloadFilesHandler.Handler(fileIds.ToList(), cancellationToken);

                if (res.IsFailure)
                {
                    return Results.BadRequest(res.Error);
                }

                return Results.Ok(res.Value);
            });
        }
    }

    public class DownloadFilesHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<DownloadFilesHandler> _logger;
        private readonly IS3Provider _s3Provider;

        public DownloadFilesHandler(IMediaRepository mediaRepository, ILogger<DownloadFilesHandler> logger, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
        }

        public async Task<Result<IReadOnlyList<GetFileDto>, Errors>> Handler(List<Guid> ids, CancellationToken cancellationToken)
        {
            if (ids.Count > 0)
            {
                _logger.LogInformation("id не указан");
                return GeneralErrors.ValueNotValid("id null").ToErrors();
            }

            var resMediaAsset = await _mediaRepository.GetBys(q => ids.Contains(q.Id), cancellationToken);
            if (resMediaAsset.IsFailure)
            {
                _logger.LogInformation("один из файлов не найден");
                return GeneralErrors.ValueNotFound(ids[0]).ToErrors();

            }

            List<GetFileDto> filesDto = new();

            foreach (var mediaAsset in resMediaAsset.Value)
            {

                var res = await _s3Provider.GenerateDownloadUrlAsync(mediaAsset.StorageKey, cancellationToken);
                if (res.IsFailure)
                {
                    _logger.LogInformation("неудалось сгенерировать ссылку на файл из s3 с одним из id");
                    return Error.NotFound("GenerateDownloadUrlAsync.IsFailure.S3", "ошибка генерации ссылки на файл из S3").ToErrors();
                }
                else
                {
                    filesDto.Add(new GetFileDto(mediaAsset.Id, res.Value, mediaAsset.StatusMedia.ToString()));
                }
            }

            return filesDto;
        }
    }
}
