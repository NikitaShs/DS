using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.S3
{
    public static class DependencyInjectionS3Extentions
    {
        public static IServiceCollection AddS3(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<S3Options>(configuration.GetSection(nameof(S3Options)));

            services.AddSingleton<IAmazonS3>(sp =>
            {
                S3Options s3Options = sp.GetRequiredService<IOptions<S3Options>>().Value;

                var configure = new AmazonS3Config
                {
                    ServiceURL = s3Options.Endpoint,
                    UseHttp = !s3Options.WithSsl,
                    ForcePathStyle = true
                };

                return new AmazonS3Client(s3Options.AccessKey, s3Options.SecretKey, configure);
            });

            return services;
        }
    }
}
