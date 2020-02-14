namespace Cloud.Framework.MicroService.Health
{
    /// <summary>
    /// 
    /// </summary>
    public static class HealthCheckConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public static class Routes
        {
            /// <summary>
            /// 
            /// </summary>
            public const string GoodToGo = "/service/healthcheck/gtg";
            
            /// <summary>
            /// 
            /// </summary>
            public const string Drain = "/service/drain";
            
            /// <summary>
            /// 
            /// </summary>
            public const string UnDrain = "/service/undrain";
        }

        internal static class Tags
        {
            public const string GoodToGo = "gtg";
            public const string Drain = "drain";
            public const string UnDrain = "undrain";
        }
    }
}