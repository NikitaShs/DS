using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.S3.BackgroundServise
{
    public class S3BucketInitializationServise : BackgroundService
    {

        private readonly IAmazonS3 _s3Client;
        private readonly IOptions<S3Options> _s3Options;
        private readonly ILogger<S3BucketInitializationServise> _logger;

        public S3BucketInitializationServise(IAmazonS3 s3Client, IOptions<S3Options> s3Options, ILogger<S3BucketInitializationServise> logger)
        {
            _s3Client = s3Client;
            _s3Options = s3Options;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                if(_s3Options.Value.RequireBuckets.Count <= 0)
                {
                    _logger.LogInformation("список бакетов пуст");
                    throw new ArgumentException("RequireBuckets required");
                }

                var creats = _s3Options.Value.RequireBuckets.Select(q => CreateBucketAsync(q, cancellationToken)).ToArray();

                await Task.WhenAll(creats);

            }
            catch(Exception e)
            {
                _logger.LogInformation("buckets не созданы");
                throw;
            }
        }

        private async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken)
        {
            try
            {

                var buckeExist = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);

                if (buckeExist)
                {
                    _logger.LogInformation("{bucketName} уже существует", bucketName);
                    return;
                }

                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName
                };
                await _s3Client.PutBucketAsync(putBucketRequest, cancellationToken);

                _logger.LogInformation("{bucketName} успешно создан", bucketName);

                string policy = $$"""
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Effect": "Allow",
            "Principal": "*",
            "Action": ["s3:GetObject"],
            "Resource": ["arn:aws:s3:::{{bucketName}}/*"]
        }
    ]
}
""";

                var putBucketPolicyRequest = new PutBucketPolicyRequest
                {
                    BucketName = bucketName,
                    Policy = policy
                };

                await _s3Client.PutBucketPolicyAsync(putBucketPolicyRequest);

                _logger.LogInformation("У {bucketName} успешно обновлена политика", bucketName);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
