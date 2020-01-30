namespace Cloud.Framework.MicroService.Health
{
    internal static class HealthCheckConstants
    {
        internal static class Routes
        {
            public const string Status = "/service/status";
            public const string GoodToGo = "/service/healthcheck/gtg";
            public const string Drain = "/service/drain";
            public const string UnDrain = "/service/undrain";
        }

        internal static class Tags
        {
            public const string GoodToGo = "gtg";
            public const string Status = "status";
            public const string Drain = "drain";
            public const string UnDrain = "undrain";
        }
    }
}