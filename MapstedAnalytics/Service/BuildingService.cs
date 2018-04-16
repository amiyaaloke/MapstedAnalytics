using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MapstedAnalytics.Models;
using Newtonsoft.Json;

namespace MapstedAnalytics.Service
{
    public class BuildingService : IBuildingService
    {
        private HttpClient _Client = new HttpClient();

        public BuildingService()
        {
            _Client.BaseAddress = new Uri("http://jobs.mapsted.com/api/Values/GetBuildingData");
            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IList<Building>> GetBuildingsAsync()
        {
            IList<Building> buildings = new List<Building>();
            HttpResponseMessage response = await _Client.GetAsync(_Client.BaseAddress.PathAndQuery);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                buildings = JsonConvert.DeserializeObject<List<Building>>(json);

            }

            return buildings;

        }
    }
}
