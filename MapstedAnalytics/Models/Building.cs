using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapstedAnalytics.Models
{
    public class Building
    {
        [JsonProperty("building_id")]
        public int BuildingId { get; set; }
        [JsonProperty("building_name")]
        public string BuildingName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
