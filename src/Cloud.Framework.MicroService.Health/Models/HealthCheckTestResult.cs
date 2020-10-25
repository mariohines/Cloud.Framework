using System;
using System.Runtime.Serialization;

namespace Cloud.Framework.MicroService.Health.Models
{
    /// <summary>
    /// Result of a health check test.
    /// </summary>
    public class HealthCheckTestResult
    {
        /// <summary>
        /// Number of milliseconds taken to run the test.
        /// </summary>
        [DataMember(Name = "duration_millis")]
        public float DurationMilliseconds { get; set; }
        
        /// <summary>
        /// The name of the test, a name that is meaningful to supporting engineers.
        /// </summary>
        [DataMember(Name = "test_name")]
        public string? TestName { get; set; }
        
        /// <summary>
        /// The state of the test.
        /// </summary>
        [DataMember(Name = "test_result")]
        public TestResult TestResult { get; set; }
        
        /// <summary>
        /// The time at with this test was executed.
        /// </summary>
        [DataMember(Name = "tested_at")]
        public DateTime TestedAt { get; set; }
    }
}