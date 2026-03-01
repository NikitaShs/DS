using Amazon.Util;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using FileService.Domain.VO;
using FileServise.Communication;
using FileServise.IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exseption;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;

namespace FileServise.IntegrationTests.Feateres
{
    public class MultipartUploadFileTests : FileServiceTestsBase
    {
        public MultipartUploadFileTests(IntegrationTestsWebFactory factory) : base(factory){}

        [Fact]
        public async Task MultipartUploadFile_FullCycle_PersistsMediaFile()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            FileInfo fileinfo = new(Path.Combine(AppContext.BaseDirectory, "Resources", "pypypy.mp4"));

            var request = new CreateFileRequest(Guid.NewGuid(), "ss", "video", fileinfo.Length, "video/mp4", fileinfo.Name);

            var result = await StartMultipartUpload(request, cancellationToken);

            await using var stream = fileinfo.OpenRead();

            var partETags = new List<PartETagDto>();

            foreach (var item in result.Value.chunkUrls)
            {
                var chunk = new byte[result.Value.chunkSize];
                int bytesRead = await stream.ReadAsync(chunk.AsMemory(0, result.Value.chunkSize), cancellationToken);

                var content = new ByteArrayContent(chunk);

                var resp = await HttpClient.PutAsync(item.UploadUrl, content, cancellationToken);

                partETags.Add(new PartETagDto(item.PartNumber, resp.Headers.ETag?.Tag.Trim('"')!));
            }

            var requestComplete = new CompleteMultipartUploadRequest(result.Value.MediaAssetId, result.Value.UploadId, partETags);

            HttpResponseMessage responseComplete = await AppHttpClient.PostAsJsonAsync("files/complete_multipartupload", requestComplete);

            var resultComplete = await responseComplete.HandlerResponseAsync<Guid>(cancellationToken);

            Assert.True(result.IsSuccess);

            await ExecuteInDb(async q => {
                var res = await q.MediaAsset.FirstOrDefaultAsync(w => w.Id == result.Value.MediaAssetId);
                Assert.NotNull(res);
                Assert.Equal(StatusMedia.UPLOADING, res.StatusMedia);
            });

        }

        [Fact]
        public async Task AbortMultipartUpload_AfterStart()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            var request = new CreateFileRequest(Guid.NewGuid(), "ss", "video", 10000, "video/mp4", "rqq.mp4");

            var result = await StartMultipartUpload(request, cancellationToken);

            var requestAbort = new AbortMultipartUploadRequest(result.Value.MediaAssetId, result.Value.UploadId);

            HttpResponseMessage response = await AppHttpClient.PostAsJsonAsync("files/termination_multipart_upload", requestAbort);

            var resultAbort = await response.HandlerResponseAsync<Guid>(cancellationToken);

            Assert.True(resultAbort.IsSuccess);

            await ExecuteInDb(async q =>
            {
                var res = await q.MediaAsset.FirstOrDefaultAsync(w => w.Id == requestAbort.MediaAssetId && w.IsActive == false);

                Assert.NotNull(res);

                Assert.Equal(StatusMedia.FAILED, res.StatusMedia);

            });

        }

        [Fact]
        public async Task GetChunkUploadUrl_AfterStart()
        {

            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            var request = new CreateFileRequest(Guid.NewGuid(), "ss", "video", 10000, "video/mp4", "rqq.mp4");

            var result = await StartMultipartUpload(request, cancellationToken);

            var requestGetChunk = new GetChunkUploadUrlRequest(2, result.Value.MediaAssetId, result.Value.UploadId);

            HttpResponseMessage response = await AppHttpClient.PostAsJsonAsync("files/multipart_upload_one_chunk", requestGetChunk);

            var resultGetChunk = await response.HandlerResponseAsync<ChunkUploadUrl>(cancellationToken);

            Assert.True(resultGetChunk.IsSuccess);

            Assert.NotNull(resultGetChunk.Value.UploadUrl);

            FileInfo fileinfo = new(Path.Combine(AppContext.BaseDirectory, "Resources", "pypypy.mp4"));

            await using var stream = fileinfo.OpenRead();


            var chunk = new byte[result.Value.chunkSize];

            int bytesRead = await stream.ReadAsync(chunk.AsMemory(0, result.Value.chunkSize), cancellationToken);

            var content = new ByteArrayContent(chunk);

            var resp = await HttpClient.PutAsync(resultGetChunk.Value.UploadUrl, content, cancellationToken);

            Assert.True(resp.IsSuccessStatusCode);

        }

        private async Task<Result<MultipartUrlsDto, Error>> StartMultipartUpload(CreateFileRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await AppHttpClient.PostAsJsonAsync("files/multipart_upload", request);

            var result = await response.HandlerResponseAsync<MultipartUrlsDto>(cancellationToken);

            Assert.True(result.IsSuccess);

            await ExecuteInDb(async q => {
                var res = await q.MediaAsset.FirstOrDefaultAsync(w => w.Id == result.Value.MediaAssetId);

                Assert.NotNull(res);

                Assert.Equal(StatusMedia.UPLOADED, res.StatusMedia);
            });

            return result;
        }
    }
}
