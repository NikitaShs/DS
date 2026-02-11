using Core.Adstract;
using CSharpFunctionalExtensions;
using FileService.Contracts;
using FileService.Core.abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;

namespace FileService.Core.Features
{
    public class AbortMultipartUpload : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/termination_multipart_upload", async (
                [FromBody] AbortMultipartUploadRequest abortMultipartUploadRequest,
                [FromServices] AbortMultipartUploadHandler abortMultipartUploadHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await abortMultipartUploadHandler.Handler(abortMultipartUploadRequest, cancellationToken);

                if (res.IsFailure)
                {
                    return Results.BadRequest(res.Error);
                }

                return Results.Ok(res.Value);
            });
        }
    }

    public class AbortMultipartUploadHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<AbortMultipartUploadHandler> _logger;
        private readonly IS3Provider _s3Provider;

        public AbortMultipartUploadHandler(IMediaRepository mediaRepository, ILogger<AbortMultipartUploadHandler> logger, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
        }

        public async Task<Result<Guid, Errors>> Handler(AbortMultipartUploadRequest abortMultipartUploadRequest, CancellationToken cancellationToken)
        {
            if (abortMultipartUploadRequest.MediaAssetId == Guid.Empty)
            {
                _logger.LogInformation("MediaAssetId не указан");
                return GeneralErrors.ValueNotValid("id null").ToErrors();
            }

            if (abortMultipartUploadRequest.UploudId == null)
            {
                _logger.LogInformation("UploudId не указан");
                return GeneralErrors.ValueNotValid("id null").ToErrors();
            }

            var mediaAsset = await _mediaRepository.GetBy(q => q.Id == abortMultipartUploadRequest.MediaAssetId, cancellationToken);
            if (mediaAsset.IsFailure)
            {
                _logger.LogInformation("нету файла с id {idFile}", abortMultipartUploadRequest.MediaAssetId);
                return GeneralErrors.ValueNotFound(abortMultipartUploadRequest.MediaAssetId).ToErrors();
            }

            var res = await _s3Provider.AbortMultipartUploadAsync(mediaAsset.Value.StorageKey, abortMultipartUploadRequest.UploudId, cancellationToken);
            if (res.IsFailure)
            {
                _logger.LogInformation("неудалось отменить загрузку файла с id {idFile}", abortMultipartUploadRequest.MediaAssetId);
                return Error.NotFound("AbortMultipartUploadAsync.IsFailure.S3", "ошибка отмены загрузки").ToErrors();
            }

            mediaAsset.Value.UpdateStatusToFail();

            var resSave = await _mediaRepository.SaveChangeAsync(cancellationToken);
            if (resSave.IsFailure)
                return Error.NotFound("SaveChangeAsync.IsFailure", "ошибка с SaveChangeAsync").ToErrors();


            return abortMultipartUploadRequest.MediaAssetId;
        }
    }
}
