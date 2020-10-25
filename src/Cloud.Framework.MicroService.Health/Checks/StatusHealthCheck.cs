using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;

namespace Cloud.Framework.MicroService.Health.Checks
{
    /// <summary>
    /// The Status resource returns information about the service.
    /// </summary>
    public sealed class StatusHealthCheck : IHealthCheck
    {
        private readonly Status _currentStatus;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentStatus">The current status of the system.</param>
        public StatusHealthCheck(Status currentStatus) {
            _currentStatus = currentStatus;
        }
        
        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken()) {
            return await Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, data: JObject.FromObject(_currentStatus).ToObject<Dictionary<string, object>>()));
        }
    }
}