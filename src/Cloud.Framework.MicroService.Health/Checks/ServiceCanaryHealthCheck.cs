using System.Threading;
using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Abstract;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cloud.Framework.MicroService.Health.Checks
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ServiceCanaryHealthCheck : IHealthCheck
    {
        private readonly IHealthStatusFlag _healthStatusFlag;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="healthStatusFlag"></param>
        public ServiceCanaryHealthCheck(IHealthStatusFlag healthStatusFlag) {
            _healthStatusFlag = healthStatusFlag;
        }
        
        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken()) {
            return await Task.FromResult(new HealthCheckResult(_healthStatusFlag.CurrentHealth));
        }
    }
}