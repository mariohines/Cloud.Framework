using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Cloud.Framework.MicroService.Health.Models
{
    /// <summary>
    /// Model to return information about the service.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Artifact name.
        /// </summary>
        [DataMember(Name = "artifact_id")]
        public string? ArtifactId { get; set; }
        
        /// <summary>
        /// The build pipeline number.
        /// </summary>
        [DataMember(Name = "build_number")]
        public string? BuildNumber { get; set; }
        
        /// <summary>
        /// The machine the artifact was built or verified on.
        /// </summary>
        [DataMember(Name = "build_machine")]
        public string? BuildMachine { get; set; }
        
        /// <summary>
        /// The user that did the build.
        /// </summary>
        [DataMember(Name = "built_by")]
        public string? BuiltBy { get; set; }
        
        /// <summary>
        /// When the build was done.
        /// </summary>
        [DataMember(Name = "built_when")]
        public DateTime BuiltWhen { get; set; }
        
        /// <summary>
        /// The compiler version.
        /// </summary>
        [DataMember(Name = "compiler_version")]
        public string? CompilerVersion { get; set; }
        
        /// <summary>
        /// The current time (time of request).
        /// </summary>
        [DataMember(Name = "current_time")]
        public DateTime CurrentTime => DateTime.UtcNow;
        
        /// <summary>
        /// The git sha1 that can be used to identify the primary material for the build.
        /// </summary>
        [DataMember(Name = "git_sha1")]
        public string? GitSha1 { get; set; }
        
        /// <summary>
        /// The name of the machine responding to this request.
        /// </summary>
        [DataMember(Name = "machine_name")]
        public string MachineName => Environment.MachineName;

        /// <summary>
        /// The architecture of the OS of the machine responding to the request.
        /// </summary>
        [DataMember(Name = "os_arch")]
        public string OsArchitecture => RuntimeInformation.OSArchitecture.ToString();

        /// <summary>
        /// The name of the OS of the machine responding to the request.
        /// </summary>
        [DataMember(Name = "os_name")]
        public string OsName => RuntimeInformation.OSDescription;

        /// <summary>
        /// The number of processors of the machine responding to the request.
        /// </summary>
        [DataMember(Name = "os_numprocessors")]
        public string OsNumberOfProcessors => Environment.ProcessorCount.ToString();

        /// <summary>
        /// The version of the OS of the machine responding to the request.
        /// </summary>
        [DataMember(Name = "os_version")]
        public string OsVersion => Environment.OSVersion.Version.ToString();

        /// <summary>
        /// How long the service responding to the request has been up.
        /// </summary>
        /// <example>50000 milliseconds</example>
        [DataMember(Name = "up_duration")]
        public string? UpDuration => $"{Process.GetCurrentProcess().StartTime.Subtract(DateTime.UtcNow).TotalMilliseconds} milliseconds";

        /// <summary>
        /// The time at which the service was started.
        /// </summary>
        [DataMember(Name = "up_since")]
        public DateTime UpSince => Process.GetCurrentProcess().StartTime;

        /// <summary>
        /// The version of the service responding to the request.
        /// </summary>
        [DataMember(Name = "version")]
        public string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}