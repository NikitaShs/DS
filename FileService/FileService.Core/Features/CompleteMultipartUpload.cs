using Core.Adstract;
using Core.Validation;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using FileService.Core.abstractions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SharedKernel.Exseption;

namespace FileService.Core.Features
{

    public class CompleteMultipartUploadValidator : AbstractValidator<CompleteMultipartUploadRequest>
    {
        public CompleteMultipartUploadValidator()
        {
            RuleFor(q => q.ETags).NotEmpty();
            RuleFor(q => q.MediaAssetId).NotEmpty();
            RuleFor(q => q.UploadId).NotEmpty();
        }
    }

    public class CompleteMultipartUpload : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/complete_multipartupload", async (
                [FromBody] CompleteMultipartUploadRequest completeMultipartUploadRequest,
                [FromServices] CompleteMultipartUploadHandler completeMultipartUploadHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await completeMultipartUploadHandler.Handler(completeMultipartUploadRequest, cancellationToken);

                if (res.IsFailure)
                {
                    return Results.BadRequest(res.Error);
                }

                return Results.Ok(res.Value);
            });
        }
    }

    public class CompleteMultipartUploadHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<CompleteMultipartUploadHandler> _logger;
        private readonly IS3Provider _s3Provider;
        private readonly CompleteMultipartUploadValidator _validator;

        public CompleteMultipartUploadHandler(
            IMediaRepository mediaRepository,
            ILogger<CompleteMultipartUploadHandler> logger,
            IS3Provider s3Provider,
            CompleteMultipartUploadValidator validator
            )
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
            _validator = validator;
        }

        public async Task<Result<Guid, Errors>> Handler(CompleteMultipartUploadRequest completeMultipartUploadRequest, CancellationToken cancellationToken)
        {

            var resValidator = _validator.Validate(completeMultipartUploadRequest);
            if(!resValidator.IsValid)
            {
                _logger.LogInformation("невалидные данные");
                return resValidator.ToList();
            }

            var mediaAsset = await _mediaRepository.GetBy(q => q.Id == completeMultipartUploadRequest.MediaAssetId, cancellationToken);
            if (mediaAsset.IsFailure)
            {
                _logger.LogInformation("нету файла с id {idFile}", completeMultipartUploadRequest.MediaAssetId);
                return GeneralErrors.ValueNotFound(completeMultipartUploadRequest.MediaAssetId).ToErrors();
            }

            var res = await _s3Provider.CompleteMultipartUploadAsync(
                mediaAsset.Value.StorageKey,
                mediaAsset.Value.MediaData,
                completeMultipartUploadRequest.UploadId,
                completeMultipartUploadRequest.ETags,
                cancellationToken);

            if (res.IsFailure)
            {
                _logger.LogInformation("неудалось завершить Multipar загрузку с id {MediaAssetId}", completeMultipartUploadRequest.MediaAssetId);
                return Error.NotFound("completeMultipartUploadRequest.IsFailure.S3", "ошибка с завершением Multipar загрузки").ToErrors();
            }

            mediaAsset.Value.UpdateStatusToUpoading();

            var resUpdateStatus = await _mediaRepository.SaveChangeAsync(cancellationToken);
            if (resUpdateStatus.IsFailure)
                return Error.NotFound("SaveChangeAsync.IsFailure", "ошибка с SaveChangeAsync").ToErrors();


            return completeMultipartUploadRequest.MediaAssetId;
        }
    }
}
