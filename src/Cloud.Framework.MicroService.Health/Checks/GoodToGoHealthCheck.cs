using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Abstract;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cloud.Framework.MicroService.Health.Checks
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GoodToGoHealthCheck : IHealthCheck
    {
        private readonly IHealthStatusFlag _healthStatusFlag;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="healthStatusFlag"></param>
        public GoodToGoHealthCheck(IHealthStatusFlag healthStatusFlag)
        {
            _healthStatusFlag = healthStatusFlag ?? throw new ArgumentNullException(nameof(healthStatusFlag));
        }

        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await Task.FromResult(new HealthCheckResult(_healthStatusFlag.StatusCode == HttpStatusCode.OK ? HealthStatus.Healthy : HealthStatus.Unhealthy));
        }
    }
}