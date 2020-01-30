using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Abstract;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cloud.Framework.MicroService.Health.Checks
{
    public sealed class UnDrainHealthCheck : IHealthCheck
    {
        private readonly IHealthStatusFlag _healthStatusFlag;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="healthStatusFlag"></param>
        public UnDrainHealthCheck(IHealthStatusFlag healthStatusFlag)
        {
            _healthStatusFlag = healthStatusFlag ?? throw new ArgumentNullException(nameof(healthStatusFlag));
        }

        /// <inheritdoc />
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            _healthStatusFlag.StatusCode = HttpStatusCode.OK;
            return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, "OK"));
        }
    }
}