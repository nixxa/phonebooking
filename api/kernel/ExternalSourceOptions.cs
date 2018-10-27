using System.Collections.Generic;

namespace Kernel
{
    public class ExternalSourceOptions
    {
        public string GetDeviceUri { get; set;}
        public string Token { get; set; }
        public IEnumerable<string> ModelNames { get; set; }
        public int CacheExpirationHours { get; set; }
        public int RateLimitPauseSeconds { get; set; }
    }
}