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
    public class DeviceUsageService : IDeviceUsageService
    {
        private HttpClient client = new HttpClient();

        public DeviceUsageService()
        {
            client.BaseAddress = new Uri("http://jobs.mapsted.com/api/Values/GetAnalyticsData");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async Task<IList<DeviceUsage>> GetDeviceUsagesAsync()
        {
            IList<DeviceUsage> deviceUsages = new List<DeviceUsage>();
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.PathAndQuery);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                deviceUsages = JsonConvert.DeserializeObject<List<DeviceUsage>>(json);

            }

            return deviceUsages;
        }
    }
}
