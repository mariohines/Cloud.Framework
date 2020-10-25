using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Abstract;
using Cloud.Framework.MicroService.Health.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;

namespace Cloud.Framework.MicroService.Health.Checks
{
    /// <summary>
    /// The HealthCheck resource provides information about internal health and its perceived health of downstream dependencies.
    /// It is up for the implementation of this specification to describe how a given HealthCheck resource may affect the current state of the GTG and/or ASG resources, or neither.
    /// </summary>
    /// <remarks>Important: the HealthCheck resource must not block waiting for HealthCheck probes to execute, it should return the last known status.</remarks>
    public sealed class ResourceHealthCheck : IHealthCheck
    {
        private readonly IEnumerable<IHealthTest> _healthTests;
        private readonly IHealthStatusFlag _healthStatusFlag;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="healthTests">Collection of custom health tests to be run.</param>
        /// <param name="healthStatusFlag">The status flag.</param>
        public ResourceHealthCheck(IEnumerable<IHealthTest> healthTests, IHealthStatusFlag healthStatusFlag) {
            _healthTests = !healthTests.Any() ? throw new ArgumentException("At least 1 test must exist to use this health check.") : healthTests;
            _healthStatusFlag = healthStatusFlag;
        }
        
        /// <inheritdoc />
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken()) {
            var result = new HealthCheckResource();
            var timer = Stopwatch.StartNew();
            foreach (var healthTest in _healthTests) {
                result.Tests.Add(await healthTest.RunTestAsync());
            }
            timer.Stop();
            result.ReportAsOf = DateTime.UtcNow;
            result.ReportDuration = $"{timer.Elapsed.TotalSeconds} seconds";
            _healthStatusFlag.SetHealthStatus(result.Tests.Any(t => t.TestResult == TestResult.Failed) ? HealthStatus.Unhealthy : HealthStatus.Healthy);
            return new HealthCheckResult(_healthStatusFlag.CurrentHealth, data: JObject.FromObject(result).ToObject<Dictionary<string, object>>());
        }
    }
}