using Amazon.S3.Model;
using Core.Adstract;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using FileService.Core.abstractions;
using FileService.Domain.Entites;
using FileService.Domain.VO;
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
    public class GetChunkUploadUrl : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("files/multipart_upload_one_chunk", async (
                [FromBody] GetChunkUploadUrlRequest getChunkUploadUrlRequest,
                [FromServices] GetChunkUploadUrlHandler getChunkUploadUrlHandler,
                CancellationToken cancellationToken) =>
            {
                var res = await getChunkUploadUrlHandler.Handler(getChunkUploadUrlRequest, cancellationToken);

                if (res.IsFailure)
                    return Results.BadRequest(res.Error);
                return Results.Ok(res.Value);
            });
        }
    }

    public class GetChunkUploadUrlHandler
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<GetChunkUploadUrlHandler> _logger;
        private readonly IS3Provider _s3Provider;

        public GetChunkUploadUrlHandler(
            IMediaRepository mediaRepository,
            ILogger<GetChunkUploadUrlHandler> logger,
            IS3Provider s3Provider)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
            _s3Provider = s3Provider;
        }

        public async Task<Result<ChunkUploadUrl, Errors>> Handler(GetChunkUploadUrlRequest getChunkUploadUrlRequest, CancellationToken cancellationToken)
        {
            if (getChunkUploadUrlRequest.PartNumber <= 0)
                return GeneralErrors.ValueNotValid("PartNumber").ToErrors();

            if (getChunkUploadUrlRequest.UploadId == null)
            {
                _logger.LogInformation("UploudId не указан");
                return GeneralErrors.ValueNotValid("id null").ToErrors();
            }

            var mediaAsset = await _mediaRepository.GetBy(q => q.Id == getChunkUploadUrlRequest.MediaAssetId, cancellationToken);
            if (mediaAsset.IsFailure)
            {
                _logger.LogInformation("нету файла с id {idFile}", getChunkUploadUrlRequest.MediaAssetId);
                return GeneralErrors.ValueNotFound(getChunkUploadUrlRequest.MediaAssetId).ToErrors();
            }

            var resultUrl = await _s3Provider.GenerateChunkUploadUrl(
                mediaAsset.Value.StorageKey,
                getChunkUploadUrlRequest.UploadId,
                getChunkUploadUrlRequest.PartNumber,
                cancellationToken);
            if (resultUrl.IsFailure)
                return Error.Failure("GenerateChunkUploadUrl.IsFailure", "неудалось сгенерировать ссылку для Multipart загрузки").ToErrors();


            return new ChunkUploadUrl(resultUrl.Value.PartNumber, resultUrl.Value.UploadUrl);
        }
    }




}
