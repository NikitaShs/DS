using Core.Adstract;
using Core.Validation;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
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
    public class UploadFileValidator : AbstractValidator<CreateFileRequest>
    {
        public UploadFileValidator()
        {
            RuleFor(q => q.Context).NotEmpty();
            RuleFor(q => q.AssetType).NotEmpty();
            RuleFor(q => q.ContentType).NotEmpty();
            RuleFor(q => q.EntiteId).NotEmpty();
            RuleFor(q => q.FileName).NotEmpty();
            RuleFor(q => q.Size).NotEmpty().GreaterThan(0);
        }
    }

    public sealed class UploadUrlFile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/upload", async (
                [FromBody] CreateFileRequest createFileRequest,
                [FromServices] UploadFileHandler uploadFileHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await uploadFileHandler.Handler(createFileRequest, cancellationToken);

                if (res.IsFailure)
                    return Results.BadRequest(res.Error);
                return Results.Ok(res.Value);
            });
        }
    }

    public class UploadFileHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<UploadFileHandler> _logger;
        private readonly UploadFileValidator _validator;
        private readonly IS3Provider _s3Provider;

        public UploadFileHandler(IMediaRepository mediaRepository, ILogger<UploadFileHandler> logger, UploadFileValidator validator, IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _validator = validator;
            _s3Provider = s3Provider;
        }

        public async Task<Result<UploadFileHandlerDto, Errors>> Handler(CreateFileRequest createFileRequest, CancellationToken cancellationToken)
        {
            var res = _validator.Validate(createFileRequest);

            if (!res.IsValid)
            {
                _logger.LogInformation("невалидные данные");
                return res.ToList();
            }

            var fileName = FileName.Create(createFileRequest.FileName);
            if (fileName.IsFailure)
                return GeneralErrors.ValueFailure("fileName").ToErrors();

            var contentType = ContentType.Create(createFileRequest.ContentType);
            if (contentType.IsFailure)
                return GeneralErrors.ValueFailure("ContentType").ToErrors();

            var mediaData = MediaData.Create(
                fileName.Value,
                contentType.Value,
                createFileRequest.Size, 1);

            if (mediaData.IsFailure)
                return GeneralErrors.ValueFailure("MediaData").ToErrors();

            var mediaOvner = MediaOwner.Create(createFileRequest.EntiteId, createFileRequest.Context);
            if (mediaOvner.IsFailure)
                return GeneralErrors.ValueFailure("MediaOwner").ToErrors();

            var mediaAssetResult = MediaAsset.CreateTypedMediaAsset(createFileRequest.AssetType.AssetTypeConvetToString(), mediaData.Value, mediaOvner.Value);

            if(mediaAssetResult.IsFailure)
                return GeneralErrors.ValueFailure("mediaAsset").ToErrors();
            var mediaAsset = mediaAssetResult.Value;

            var result = await _mediaRepository.AddFileAsync(mediaAsset, cancellationToken);
            if(result.IsFailure)
                return Error.Failure("CreateFileAsync.Failure", "файл не создан в БД").ToErrors();

            var urlResult = await _s3Provider.GenerateUploadUrlAsync(mediaAsset.StorageKey, mediaAsset.MediaData, cancellationToken);
            if (urlResult.IsFailure)
                return Error.Failure("GenerateUploadUrl.Failure", "ссылка на добовление фото не сгенерированно").ToErrors();

            return new UploadFileHandlerDto(urlResult.Value, result.Value);
        }
    }

}