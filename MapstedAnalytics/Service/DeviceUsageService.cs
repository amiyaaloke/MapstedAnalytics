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
        private HttpClient _Client = new HttpClient();
        private IList<DeviceUsage> _DeviceUsage = null;
        private IList<Building> _Buildings = null;

        public DeviceUsageService()
        {
            _Client.BaseAddress = new Uri("http://jobs.mapsted.com/api/Values/GetAnalyticsData");
            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IList<DeviceUsage>> GetDeviceUsagesAsync()
        {
            _DeviceUsage = new List<DeviceUsage>();
            HttpResponseMessage response = await _Client.GetAsync(_Client.BaseAddress.PathAndQuery);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                _DeviceUsage = JsonConvert.DeserializeObject<List<DeviceUsage>>(json);

            }

            _Buildings = await new BuildingService().GetBuildingsAsync();

            return _DeviceUsage;
        }

        public double GetTotalPurchaseCostPerManufacturer(string manufacturer)
        {
            var totalCost = _DeviceUsage.Where(x => x.Manufacturer.ToLower() == manufacturer.ToLower())
                .Sum(x => x.UsageStatistics.SessionInfos.Sum(y => y.Purchases.Sum(z => z.Cost)));

            return totalCost;
        }

        public int GetCountOfPurchaseByItemId(int itemId)
        {
            var count = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         from purchase in session.Purchases
                         where purchase.ItemId == itemId
                         select item).Count();

            return count;
        }

        public int GetCountOfPurchaseByItemCategoryId(int itemCategoyId)
        {
            var count = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         from purchase in session.Purchases
                         where purchase.ItemCategoryId == itemCategoyId
                         select item).Count();

            return count;
        }

        public double GetTotalPurchaseCostByState(string state)
        {
            var buildingIds = from building in _Buildings
                              where building.State.ToLower() == state.ToLower()
                              select building.BuildingId;

            var items = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         where (from building in _Buildings where building.State.ToLower() == state.ToLower() select building.BuildingId)
                         .Contains(session.BuildingId)
                         select session.Purchases);

            var totalCost = items.Sum(i => i.Sum(j => j.Cost));

            return totalCost;
        }

        public double GetTotalPurchaseCostByCountry(string country)
        {
            var buildingIds = from building in _Buildings
                              where building.Country.ToLower() == country.ToLower()
                              select building.BuildingId;

            var items = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         where (from building in _Buildings where building.Country.ToLower() == country.ToLower() select building.BuildingId)
                         .Contains(session.BuildingId)
                         select session.Purchases);

            var totalCost = items.Sum(i => i.Sum(j => j.Cost));

            return totalCost;
        }

        public int GetBuildingWithMostPurchaseCost()
        {
            var items = from item in _DeviceUsage
                        from session in item.UsageStatistics.SessionInfos
                        group session.Purchases.Sum(x => x.Cost) by session.BuildingId into p
                        select new { BuildingId = p.Key, TotalCost = p.Sum() };

            var mostPurchaseCostBuildingId = items.OrderByDescending(x => x.TotalCost).First().BuildingId;

            return mostPurchaseCostBuildingId;
        }
    }
}
