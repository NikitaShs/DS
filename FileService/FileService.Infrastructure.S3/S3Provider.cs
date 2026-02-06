using Amazon.S3;
using Amazon.S3.Model;
using CSharpFunctionalExtensions;
using FileService.Core.abstractions;
using FileService.Domain.VO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.S3
{
    public class S3Provider : IS3Provider
    {
        private readonly ILogger<S3Provider> _logger;
        private readonly IAmazonS3 _s3client;
        private readonly S3Options _s3Options;

        private readonly SemaphoreSlim _requestSemaphor = new(20);

        public S3Provider(IAmazonS3 s3client, IOptions<S3Options> s3Options, ILogger<S3Provider> logger)
        {
            _s3client = s3client;
            _s3Options = s3Options.Value;
            _logger = logger;
        }

        public async Task<Result<string, Error>> StartMultipartUploadAsync(StorageKey storageKey, MediaData mediaData, CancellationToken cancellationToken)
        {
            try
            {
                var request = new InitiateMultipartUploadRequest()
                {
                    BucketName = storageKey.Bucket,
                    Key = storageKey.ValueKey,
                    ContentType = mediaData.ContentType.ValueContentType
                };

                var results = await _s3client.InitiateMultipartUploadAsync(request);
                _logger.LogInformation("InitiateMultipartUploadAsync успешно завершена");

                return results.UploadId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InitiateMultipartUploadAsync ошибка");
                return Error.Unknown("problem.in.MultipartUpload", "проблема при  частичной загрузке файлов");
            }
        }

        public async Task<Result<IReadOnlyList<string>, Error>> GenerateAllChunkUploadUrlsAsync(
            StorageKey storageKey, MediaData mediaData,
            string uploadId, int totalChunks, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = Enumerable.Range(1, totalChunks).Select(async partNumder =>
            {
                await _requestSemaphor.WaitAsync(cancellationToken);
                try
                {
                    var request = new GetPreSignedUrlRequest
                    {
                        BucketName = storageKey.Bucket,
                        Key = storageKey.ValueKey,
                        Verb = HttpVerb.PUT,
                        UploadId = uploadId,
                        PartNumber = partNumder,
                        Expires = DateTime.UtcNow.AddHours(_s3Options.UploadUrlExpirationHours),
                        Protocol = _s3Options.WithSsl ? Protocol.HTTP : Protocol.HTTPS
                    };

                    var url = await _s3client.GetPreSignedURLAsync(request);

                    return url;
                }
                finally
                {
                    _requestSemaphor.Release();

                }
            });

                var results = await Task.WhenAll(tasks);


                return results;
            }
            catch (Exception ex)
            {
                return Error.Unknown("problem.in.GenerateChunkUploadUrl", "проблема при генерации ссылок на загрузку по частям");
            }
        }

        public async Task<Result<CompleteMultipartUploadResponse, Error>> CompleteMultipartUploadAsync(
            StorageKey storageKey, MediaData mediaData,
            string uploadId, IReadOnlyList<PartETag> partETags,
            CancellationToken cancellationToken)
        {
            try
            {
                var request = new CompleteMultipartUploadRequest()
                {
                    BucketName = storageKey.Bucket,
                    Key = storageKey.ValueKey,
                    UploadId = uploadId,
                    PartETags = partETags.ToList()
                };

                var response = await _s3client.CompleteMultipartUploadAsync(request, cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                return Error.Unknown("problem.in.GenerateChunkUploadUrl", "проблема при генерации ссылок на загрузку по частям");
            }
        }

        public async Task<Result<string, Error>> GenerateDownloadUrlAsync(StorageKey storageKey, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = storageKey.Bucket,
                    Key = storageKey.ValueKey,
                    Verb = HttpVerb.GET,
                    Expires = DateTime.UtcNow.AddHours(_s3Options.UploadUrlExpirationHours),
                    Protocol = _s3Options.WithSsl ? Protocol.HTTP : Protocol.HTTPS
                };

                var response = await _s3client.GetPreSignedURLAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                return Error.Unknown("problem.in.GenerateUploadUrl", "проблема при генерации ссылки на скачивание");
            }
        }

        public async Task<Result<string, Error>> GenerateUploadUrlAsync(StorageKey storageKey, MediaData mediaData, CancellationToken cancellationToken)
        {
            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = storageKey.Bucket,
                    Key = storageKey.ValueKey,
                    Verb = HttpVerb.PUT,
                    ContentType = mediaData.ContentType.ValueContentType,
                    Expires = DateTime.UtcNow.AddHours(_s3Options.UploadUrlExpirationHours),
                    Protocol = _s3Options.WithSsl ? Protocol.HTTP : Protocol.HTTPS
                };

                var response = await _s3client.GetPreSignedURLAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                return Error.Unknown("problem.in.GenerateUploadUrl", "проблема при генерации ссылки на загрузку");
            }
        }

        public async Task<Result<string, Error>> DeleteFileAsync(StorageKey storageKey, CancellationToken cancellationToken)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = storageKey.Bucket,
                    Key = storageKey.ValueKey
                };

                var response = await _s3client.DeleteObjectAsync(request, cancellationToken);

                return response.DeleteMarker;
            }
            catch (Exception ex)
            {
                return Error.Unknown("problem.in.DeleteFile", "проблема при удалении файла");
            }
        }
    }
}
