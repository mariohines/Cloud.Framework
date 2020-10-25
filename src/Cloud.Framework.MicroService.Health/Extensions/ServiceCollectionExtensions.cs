using Cloud.Framework.MicroService.Health.Checks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cloud.Framework.MicroService.Health.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicroServiceHealthChecks(this IServiceCollection services) {
            services.AddHealthChecks()
                    .AddCheck<ResourceHealthCheck>("Resource Health Check", HealthStatus.Unhealthy, new []{HealthCheckConstants.Tags.ResourceHealthCheck})
                    .AddCheck<GoodToGoHealthCheck>("Good To Go", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.GoodToGo})
                    .AddCheck<ServiceCanaryHealthCheck>("Service Canary Health Check", HealthStatus.Unhealthy, new []{HealthCheckConstants.Tags.ServiceCanary})
                    .AddCheck<StatusHealthCheck>("Status Check", HealthStatus.Unhealthy, new []{HealthCheckConstants.Tags.Status});
            
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGoodToGoHealthCheck(this IServiceCollection services) {
            services.AddHealthChecks()
                    .AddCheck<GoodToGoHealthCheck>("Good To Go", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.GoodToGo});

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddResourceHealthCheck(this IServiceCollection services) {
            services.AddHealthChecks()
                    .AddCheck<ResourceHealthCheck>("Resource Health Check", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.ResourceHealthCheck});

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddStatusHealthCheck(this IServiceCollection services) {
            services.AddHealthChecks()
                    .AddCheck<StatusHealthCheck>("Status Check", HealthStatus.Unhealthy, new []{HealthCheckConstants.Tags.Status});

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceCanaryHealthCheck(this IServiceCollection services) {
            services.AddHealthChecks()
                    .AddCheck<ServiceCanaryHealthCheck>("Service Canary Health Check", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.ServiceCanary});

            return services;
        }
    }
}