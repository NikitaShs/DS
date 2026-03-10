using FileServise.Communication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FileService.Contracts.HttpCommunicationFile
{
    public static class FileServiseExtentionsHttpCommunication
    {
        public static IServiceCollection AddFilesService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FileServiseOptions>(configuration.GetSection(nameof(FileServiseOptions)));

            services.AddHttpClient<IFileCommunicationServise, FileHttpClient>((sp, config) =>
            {
                FileServiseOptions fileOptions = sp.GetRequiredService<IOptions<FileServiseOptions>>().Value;

                config.BaseAddress = new Uri(fileOptions.Url);

                config.Timeout = TimeSpan.FromSeconds(fileOptions.TimeoutSeconds);
            });

            return services;
        }
    }
}
