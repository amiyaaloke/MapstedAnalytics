using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapstedAnalytics.Models
{
    public class SessionInfo
    {
        [JsonProperty("building_id")]
        public int BuildingId { get; set; }

        [JsonProperty("purchases")]
        public Purchase[] Purchases { get; set; }
    }
}
