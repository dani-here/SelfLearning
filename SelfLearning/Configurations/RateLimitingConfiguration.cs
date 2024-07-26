namespace SelfLearning.Configurations
{
    public class RateLimitingConfiguration
    {
        public static readonly string ConfigSectionName = "RateLimitingConfiguration";

        public int APIPermitLimit { get; set; }

        public int RateLimitWindow { get; set; }

        public int QueueLimit { get; set; }

        public bool IsEnabled { get; set; }
    }
}
