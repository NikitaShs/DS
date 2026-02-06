using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.S3
{
    public record S3Options
    {
        public string Endpoint { get; init; } = string.Empty;

        public string AccessKey { get; init; } = string.Empty;

        public string SecretKey { get; init; } = string.Empty;

        public bool WithSsl { get; init; }

        public int DownloadUrlExpirationHours { get; init; } = 24;

        public int UploadUrlExpirationHours { get; init; } = 3;

        public IReadOnlyList<string> RequireBuckets { get; init; } = [];


    }
}
