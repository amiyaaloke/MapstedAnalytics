using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapstedAnalytics.Models
{
    public class DeviceUsage
    {
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("market_name")]
        public string MarketName { get; set; }

        [JsonProperty("codename")]
        public string CodeName { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("usage_statistics")]
        public UsageStatistics UsageStatistics { get; set; }


    }
}
