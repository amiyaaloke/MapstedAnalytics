using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapstedAnalytics.Models
{
    public class Purchase
    {
        [JsonProperty("item_id")]
        public int ItemId { get; set; }

        [JsonProperty("item_category_id")]
        public int ItemCategoryId { get; set; }

        [JsonProperty("cost")]
        public decimal Cost { get; set; }
    }
}
