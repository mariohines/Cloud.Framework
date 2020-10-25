using System;
using System.Threading;
using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Abstract;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cloud.Framework.MicroService.Health.Checks
{
    /// <summary>
    /// The "Good To Go" (GTG) returns a successful response in the case that the service is in an operational state and is able to receive traffic.
    /// This resource is used by load balancers and monitoring tools to determine if traffic should be routed to this service or not.
    /// </summary>
    public sealed class GoodToGoHealthCheck : IHealthCheck
    {
        private readonly IHealthStatusFlag _healthStatusFlag;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="healthStatusFlag">The status flag.</param>
        public GoodToGoHealthCheck(IHealthStatusFlag healthStatusFlag)
        {
            _healthStatusFlag = healthStatusFlag ?? throw new ArgumentNullException(nameof(healthStatusFlag));
        }

        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await Task.FromResult(new HealthCheckResult(_healthStatusFlag.CurrentHealth));
        }
    }
}