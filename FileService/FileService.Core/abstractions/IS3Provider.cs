using Amazon.S3.Model;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using FileService.Domain.VO;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.abstractions
{
    public interface IS3Provider
    {
        Task<Result<string, Error>> StartMultipartUploadAsync(StorageKey storageKey, MediaData mediaData, CancellationToken cancellationToken);

        Task<Result<IReadOnlyList<ChunkUploadUrl>, Error>> GenerateAllChunkUploadUrlsAsync(
            StorageKey storageKey, MediaData mediaData,
            string uploadId, int totalChunks, CancellationToken cancellationToken);

        Task<Result<CompleteMultipartUploadResponse, Error>> CompleteMultipartUploadAsync(
            StorageKey storageKey, MediaData mediaData,
            string uploadId, IReadOnlyList<PartETagDto> partETags,
            CancellationToken cancellationToken);

        Task<Result<ChunkUploadUrl, Error>> GenerateChunkUploadUrl(
            StorageKey storageKey,
            string uploadId,
            int partNumber,
            CancellationToken cancellationToken);

        Task<Result<string, Error>> AbortMultipartUploadAsync(StorageKey storageKey, string uploadId, CancellationToken cancellationToken);

        Task<Result<string, Error>> DeleteFileAsync(StorageKey storageKey, CancellationToken cancellationToken);

        Task<Result<string, Error>> GenerateDownloadUrlAsync(StorageKey storageKey, CancellationToken cancellationToken);

        Task<Result<string, Error>> GenerateUploadUrlAsync(StorageKey storageKey, MediaData mediaData, CancellationToken cancellationToken);
    }
}
