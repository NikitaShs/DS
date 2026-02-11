using Core.Adstract;
using CSharpFunctionalExtensions;
using FileService.Contracts;
using FileService.Core.abstractions;
using FileService.Domain.Entites;
using FileService.Domain.VO;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;

namespace FileService.Core.Features
{
    public sealed class DeleteFile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("files/{fileId:guid}", async(
                [FromRoute] Guid fileId,
                [FromServices] DeleteFileHandler deleteFileHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await deleteFileHandler.Handler(fileId, cancellationToken);

                if (res.IsFailure)
                {
                    return Results.BadRequest(res.Error);
                }

                return Results.Ok(res.Value);
            });
        }
    }

    public class DeleteFileHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<DeleteFileHandler> _logger;
        private readonly IS3Provider _s3Provider;

        public DeleteFileHandler(IMediaRepository mediaRepository, ILogger<DeleteFileHandler> logger, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
        }

        public async Task<Result<Guid, Errors>> Handler(Guid id, CancellationToken cancellationToken)
        {
            if(id == Guid.Empty)
            {
                _logger.LogInformation("id не указан");
                GeneralErrors.ValueNotValid("id null");
            }

            var mediaAsset = await _mediaRepository.GetBy(q => q.Id == id, cancellationToken);
            if (mediaAsset.IsFailure)
            {
                _logger.LogInformation("нету файла с id {idFile}", id);
                return GeneralErrors.ValueNotFound(id).ToErrors();
            }

            var result = await _mediaRepository.DeleteFileAsync(id, cancellationToken);
            if (result.IsFailure)
            {
                _logger.LogInformation("неудалось удалить файл в БД с id {idFile}", id);
                return Error.NotFound("DeleteFileAsync.IsFailure.Posgre", "файл не удалён из БД").ToErrors();
            }

            var res = await _s3Provider.DeleteFileAsync(mediaAsset.Value.StorageKey, cancellationToken);
            if (res.IsFailure)
            {
                _logger.LogInformation("неудалось удалить файл из s3 с id {idFile}", id);
                return Error.NotFound("DeleteFileAsync.IsFailure.S3", "файл не удалён из S3").ToErrors();
            }

            return id;
        }
    }

}