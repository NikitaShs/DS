using Core.Adstract;
using Core.Validation;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Features
{
    public class MultipartUploadFileValidator : AbstractValidator<CreateFileRequest>
    {
        public MultipartUploadFileValidator()
        {
            RuleFor(q => q.Context).NotEmpty();
            RuleFor(q => q.AssetType).NotEmpty();
            RuleFor(q => q.ContentType).NotEmpty();
            RuleFor(q => q.EntiteId).NotEmpty();
            RuleFor(q => q.FileName).NotEmpty();
            RuleFor(q => q.Size).NotEmpty();
        }
    }

    public class StartMultipartUploadFile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/multipart_upload", async (
                [FromBody] CreateFileRequest createFileRequest,
                [FromServices] MultipartUploadFileHandler multipartUploadFileHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await multipartUploadFileHandler.Handler(createFileRequest, cancellationToken);

                if (res.IsFailure)
                    return Results.BadRequest(res.Error);
                return Results.Ok(res.Value);
            });
        }
    }

    public class MultipartUploadFileHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<MultipartUploadFileHandler> _logger;
        private readonly MultipartUploadFileValidator _validator;
        private readonly IS3Provider _s3Provider;
        private readonly IChunkSizeCalculator _chunkSizeCalculator;

        public MultipartUploadFileHandler(
            IMediaRepository mediaRepository,
            ILogger<MultipartUploadFileHandler> logger,
            MultipartUploadFileValidator validator,
            IS3Provider s3Provider,
            IChunkSizeCalculator chunkSizeCalculator)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _validator = validator;
            _s3Provider = s3Provider;
            _chunkSizeCalculator = chunkSizeCalculator;
        }

        public async Task<Result<MultipartUrlsDto, Errors>> Handler(CreateFileRequest createFileRequest, CancellationToken cancellationToken)
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

            var countChunksResult = _chunkSizeCalculator.Calculator(createFileRequest.Size);

            var mediaData = MediaData.Create(
                fileName.Value,
                contentType.Value,
                createFileRequest.Size, countChunksResult.Value.TotalChunks);

            if (mediaData.IsFailure)
                return GeneralErrors.ValueFailure("MediaData").ToErrors();

            var mediaOvner = MediaOwner.Create(createFileRequest.EntiteId, createFileRequest.Context);
            if (mediaOvner.IsFailure)
                return GeneralErrors.ValueFailure("MediaOwner").ToErrors();

            var mediaAssetResult = MediaAsset.CreateTypedMediaAsset(createFileRequest.AssetType.AssetTypeConvetToString(), mediaData.Value, mediaOvner.Value);

            if (mediaAssetResult.IsFailure)
                return GeneralErrors.ValueFailure("mediaAsset").ToErrors();
            var mediaAsset = mediaAssetResult.Value;

            var resultUploadId = await _s3Provider.StartMultipartUploadAsync(mediaAsset.StorageKey, mediaAsset.MediaData, cancellationToken);
            if(resultUploadId.IsFailure)
                return Error.Failure("StartMultipartUploadAsync.IsFailure", "неудалось начать Multipart загрузку").ToErrors();

            var resAddFile = await _mediaRepository.AddFileAsync(mediaAsset, cancellationToken);
            if (resAddFile.IsFailure)
                return Error.Failure("AddFileAsync.IsFailure", "неудалось создать файл в БД").ToErrors();

            var resultUrls = await _s3Provider.GenerateAllChunkUploadUrlsAsync(mediaAsset.StorageKey, mediaAsset.MediaData, resultUploadId.Value, countChunksResult.Value.TotalChunks, cancellationToken);
            if (resultUrls.IsFailure)
                return Error.Failure("GenerateAllChunkUploadUrlsAsync.IsFailure", "неудалось сгенерировать ссылки для Multipart загрузки").ToErrors();


            return new MultipartUrlsDto(mediaAsset.Id, resultUploadId.Value, resultUrls.Value, (int)countChunksResult.Value.ChunckSize);
        }
    }
}
