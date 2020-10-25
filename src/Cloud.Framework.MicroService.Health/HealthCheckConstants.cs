namespace Cloud.Framework.MicroService.Health
{
    /// <summary>
    /// Constants for Health checks.
    /// </summary>
    public static class HealthCheckConstants
    {
        /// <summary>
        /// Static routes for health checks.
        /// </summary>
        public static class Routes
        {
            /// <summary>
            /// The GTG route.
            /// </summary>
            public const string GoodToGo = "/service/healthcheck/gtg";

            /// <summary>
            /// The Status route.
            /// </summary>
            public const string Status = "/service/status";
            
            /// <summary>
            /// The ResourceHealthCheck route.
            /// </summary>
            public const string ResourceHealthCheck = "/service/healthcheck";
            
            /// <summary>
            /// The ServiceCanary route.
            /// </summary>
            public const string ServiceCanary = "/service/healthcheck/asg";

            /// <summary>
            /// The Configuration route.
            /// </summary>
            public const string Configuration = "/service/config";
        }

        internal static class Tags
        {
            public const string GoodToGo = "gtg";
            public const string Status = "status";
            public const string ResourceHealthCheck = "healthcheck";
            public const string ServiceCanary = "asg";
        }
    }
}