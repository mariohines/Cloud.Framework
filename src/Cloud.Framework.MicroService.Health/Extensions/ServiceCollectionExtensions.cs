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
                    .AddCheck<DrainHealthCheck>("Drain", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.Drain})
                    .AddCheck<UnDrainHealthCheck>("Un-Drain", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.UnDrain})
                    .AddCheck<GoodToGoHealthCheck>("Good To Go", HealthStatus.Unhealthy, new[] {HealthCheckConstants.Tags.GoodToGo});
            
            return services;
        }
    }
}