using Core.Adstract;
using FileService.Core.abstractions;
using FileService.Core.Features;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace FileService.Core
{
    public static class DependencyInjectionCore
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(typeof(DependencyInjectionCore).Assembly);

            services.AddScoped<UploadFileHandler>();

            services.AddScoped<DownloadFileHandler>();

            services.AddScoped<DeleteFileHandler>();

            return services;

        }

        public static IServiceCollection AddEndpoints(this IServiceCollection services)
        {
            ServiceDescriptor[] serviceDescriptors = typeof(DependencyInjectionCore).Assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            IEnumerable<IEndpoint> endpoints = app.Services
                .GetRequiredService<IEnumerable<IEndpoint>>();

            IEndpointRouteBuilder builder =
                routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(builder);
            }

            return app;
        }
    }
}
