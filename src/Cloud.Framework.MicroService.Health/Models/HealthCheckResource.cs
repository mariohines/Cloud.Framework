using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Cloud.Framework.MicroService.Health.Models
{
    /// <summary>
    /// Provides information about internal health and its perceived health of downstream dependencies.
    /// </summary>
    public class HealthCheckResource
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HealthCheckResource() {
            Tests = new List<HealthCheckTestResult>();
        }
        
        /// <summary>
        /// The time at which this report was generated (this may not be the current time).
        /// </summary>
        [DataMember(Name = "report_as_of")]
        public DateTime ReportAsOf { get; set; }
        
        /// <summary>
        /// How long it took to generate the report.
        /// </summary>
        /// <example>0 seconds</example>
        [DataMember(Name = "report_duration")]
        public string? ReportDuration { get; set; }
        
        /// <summary>
        /// Array of results.
        /// </summary>
        [DataMember(Name = "tests")]
        public ICollection<HealthCheckTestResult> Tests { get; set; }
    }
}