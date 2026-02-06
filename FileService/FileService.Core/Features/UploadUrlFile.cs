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

    public sealed class UploadUrlFile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("UploadFileGenerateUrl", async ([FromBody] CreateFileRequest createFileRequest, [FromServices] UploadFileHandler uploadFileHandler, CancellationToken cancellationToken) =>
            {
                var res = await uploadFileHandler.Handler(createFileRequest, cancellationToken);

                if (res.IsFailure)
                    return Results.BadRequest(res.Error.Message);
                return Results.Ok(res.Value);
            });
        }
    }

    public class UploadFileHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<UploadUrlFile> _logger;
        private readonly UploadFileValidator _validator;
        private readonly IS3Provider _s3Provider;

        public UploadFileHandler(IMediaRepository mediaRepository, ILogger<UploadUrlFile> logger, UploadFileValidator validator, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _validator = validator;
            _s3Provider = s3Provider;
        }

        public async Task<Result<string, Error>> Handler(CreateFileRequest createFileRequest, CancellationToken cancellationToken)
        {
            var res = _validator.Validate(createFileRequest);

            if (!res.IsValid)
            {
                _logger.LogInformation("невалидные данные");
                return GeneralErrors.ValueNotValid("createFileRequest");
            }

            var fileName = FileName.Create(createFileRequest.fileName);
            if (fileName.IsFailure)
                return GeneralErrors.ValueFailure("fileName");

            var contentType = ContentType.Create(createFileRequest.ContentType);
            if (contentType.IsFailure)
                return GeneralErrors.ValueFailure("ContentType");

            var mediaData = MediaData.Create(
                fileName.Value,
                contentType.Value,
                createFileRequest.size, 1);

            if (mediaData.IsFailure)
                return GeneralErrors.ValueFailure("MediaData");

            var mediaOvner = MediaOwner.Create(createFileRequest.entiteId, createFileRequest.context);
            if (mediaOvner.IsFailure)
                return GeneralErrors.ValueFailure("MediaOwner");

            MediaAsset mediaAsset;

            switch (createFileRequest.assetType.AssetTypeConvetToString())
            {
                case AssetType.VIDEO:
                    var videoAsset = VideoAsset.CreateForUpload(Guid.NewGuid(), mediaData.Value, AssetType.VIDEO, mediaOvner.Value);
                    if (videoAsset.IsFailure)
                        return GeneralErrors.ValueFailure("VideoAsset");
                    mediaAsset = videoAsset.Value;
                    break;
                case AssetType.AVATAR:
                    _logger.LogInformation("ещё нету");
                    return GeneralErrors.Unknown();
                    break;
                case AssetType.IMAGE:
                    _logger.LogInformation("ещё нету");
                    return GeneralErrors.Unknown();
                    break;
                case AssetType.PREVIEW:
                    var previewAsset = PreviewAsset.CreateForUpload(Guid.NewGuid(), mediaData.Value, AssetType.VIDEO, mediaOvner.Value);
                    if (previewAsset.IsFailure)
                        return GeneralErrors.ValueFailure("PreviewAsset");
                    mediaAsset = previewAsset.Value;
                    break;
                default:
                    _logger.LogInformation("асет такого типа не существует");
                    return GeneralErrors.Unknown();
            }

            var result = await _mediaRepository.CreateFileAsync(mediaAsset, cancellationToken);
            if(result.IsFailure)
                return Error.Failure("CreateFileAsync.Failure", "файл не создан в БД");

            var urlResult = await _s3Provider.GenerateUploadUrlAsync(mediaAsset.StorageKey, mediaAsset.MediaData, cancellationToken);
            if (urlResult.IsFailure)
                return Error.Failure("GenerateUploadUrl.Failure", "ссылка на добовление фото не сгенерированно");

            return urlResult.Value;
        }

        public class UploadFileValidator : AbstractValidator<CreateFileRequest>
        {
            public UploadFileValidator()
            {
                RuleFor(q => q.context).NotEmpty();
                RuleFor(q => q.assetType).NotEmpty();
                RuleFor(q => q.ContentType).NotEmpty();
                RuleFor(q => q.entiteId).NotEmpty();
                RuleFor(q => q.fileName).NotEmpty();
                RuleFor(q => q.size).NotEmpty().GreaterThan(0);
            }
        }
    }

}