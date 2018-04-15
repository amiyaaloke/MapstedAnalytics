using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapstedAnalytics.Models
{
    public class UsageStatistics
    {
        [JsonProperty("session_infos")]
        public SessionInfo[] SessionInfos { get; set; }
    }
}
