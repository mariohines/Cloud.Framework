using System.Runtime.Serialization;

namespace Cloud.Framework.MicroService.Health.Models
{
    /// <summary>
    /// Test result enumeration.
    /// </summary>
    public enum TestResult
    {
        /// <summary>
        /// NotRun result.
        /// </summary>
        [DataMember(Name = "not_run")]
        NotRun = 0,
        /// <summary>
        /// Running result.
        /// </summary>
        [DataMember(Name = "running")]
        Running,
        /// <summary>
        /// Passed result.
        /// </summary>
        [DataMember(Name = "passed")]
        Passed,
        /// <summary>
        /// Failed result.
        /// </summary>
        [DataMember(Name = "failed")]
        Failed
    }
}