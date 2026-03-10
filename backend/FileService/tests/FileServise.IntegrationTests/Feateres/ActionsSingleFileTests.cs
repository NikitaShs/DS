using Amazon.Util;
using CSharpFunctionalExtensions;
using FileService.Contracts.Dtos;
using FileService.Domain.VO;
using FileServise.Communication;
using FileServise.IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exseption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileServise.IntegrationTests.Feateres
{
    public class ActionsSingleFileTests : FileServiceTestsBase
    {
        public ActionsSingleFileTests(IntegrationTestsWebFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task DeleteFile()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            var request = new CreateFileRequest(Guid.NewGuid(), "ss", "preview", 10, "images/jpeg", "rqqs.jpg");

            var res = await CreateUploadUrlFile(request, cancellationToken);

            HttpResponseMessage response = await AppHttpClient.DeleteAsync($"files/{res.Value.MediaAssetId}");

            var result = await response.HandlerResponseAsync<Guid>(cancellationToken);

            Assert.True(result.IsSuccess);

            await ExecuteInDb(async q =>
            {
                var res = await q.MediaAsset.FirstOrDefaultAsync(w => w.Id == result.Value && w.IsActive == false);

                Assert.NotNull(res);

                Assert.Equal(StatusMedia.FAILED, res.StatusMedia);

            });

        }

        [Fact]
        public async Task DownloadFile()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            FileInfo fileinfo = new(Path.Combine(AppContext.BaseDirectory, "Resources", "rqq.jpg"));

            var request = new CreateFileRequest(Guid.NewGuid(), "ss", "preview", fileinfo.Length, "images/jpeg", fileinfo.Name);

            var res = await CreateUploadUrlFile(request, cancellationToken);

            await using var stream = fileinfo.OpenRead();

            var content = new StreamContent(stream);

            content.Headers.ContentType = new MediaTypeHeaderValue("images/jpeg");

            content.Headers.ContentLength = fileinfo.Length;

            var resp = await HttpClient.PutAsync(res.Value.url, content, cancellationToken);

            Assert.True(resp.IsSuccessStatusCode);

            HttpResponseMessage response = await AppHttpClient.GetAsync($"files/{res.Value.MediaAssetId}");

            var result = await response.HandlerResponseAsync<GetFileDto>(cancellationToken);

            Assert.True(result.IsSuccess);

            Assert.NotNull(result.Value);


        }

        [Fact]
        public async Task UploadUrlFile_Simply()
        {
            CancellationToken cancellationToken = new CancellationTokenSource().Token;

            FileInfo fileinfo = new(Path.Combine(AppContext.BaseDirectory, "Resources", "rqq.jpg"));

            var request = new CreateFileRequest(Guid.NewGuid(), "ss", "preview", fileinfo.Length, "images/jpeg", fileinfo.Name);

            var res = await CreateUploadUrlFile(request, cancellationToken);

            await using var stream = fileinfo.OpenRead();

            var content = new StreamContent(stream);

            content.Headers.ContentType = new MediaTypeHeaderValue("images/jpeg");

            content.Headers.ContentLength = fileinfo.Length;

            var resp = await HttpClient.PutAsync(res.Value.url, content, cancellationToken);

            Assert.True(resp.IsSuccessStatusCode);

        }


        private async Task<Result<UploadFileHandlerDto, Error>> CreateUploadUrlFile(CreateFileRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await AppHttpClient.PostAsJsonAsync("files/upload", request);

            var result = await response.HandlerResponseAsync<UploadFileHandlerDto>(cancellationToken);

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
