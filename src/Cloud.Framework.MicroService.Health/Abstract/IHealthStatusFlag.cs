using System.Collections.Generic;
using System.Net;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cloud.Framework.MicroService.Health.Abstract
{
    /// <summary>
    /// Interface for maintaining changes in health status for different health checks.
    /// </summary>
    public interface IHealthStatusFlag
    {
        /// <summary>
        /// Allowed http status codes for health checks.
        /// </summary>
        IEnumerable<HttpStatusCode> AllowedStatusCodes { get; }
        
        /// <summary>
        /// Current status code for the health check.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }
        
        /// <summary>
        /// The current health of the system.
        /// </summary>
        HealthStatus CurrentHealth { get; }

        /// <summary>
        /// Method to set the current health of the system.
        /// </summary>
        /// <param name="status"></param>
        void SetHealthStatus(HealthStatus status);
    }
}