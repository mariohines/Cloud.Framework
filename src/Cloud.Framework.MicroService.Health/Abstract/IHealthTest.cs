using System.Threading.Tasks;
using Cloud.Framework.MicroService.Health.Checks;
using Cloud.Framework.MicroService.Health.Models;

namespace Cloud.Framework.MicroService.Health.Abstract
{
    /// <summary>
    /// Abstraction for a custom health test to be run during a <see cref="ResourceHealthCheck"/> operation.
    /// </summary>
    public interface IHealthTest
    {
        /// <summary>
        /// Method to run a health test asynchronously.
        /// </summary>
        /// <returns>A <see cref="HealthCheckTestResult"/> object.</returns>
        Task<HealthCheckTestResult> RunTestAsync();
    }
}