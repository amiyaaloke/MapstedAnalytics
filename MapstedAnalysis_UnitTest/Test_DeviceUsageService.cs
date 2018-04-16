using MapstedAnalytics.Models;
using MapstedAnalytics.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapstedAnalysis_UnitTest
{
    [TestClass]
    public class Test_DeviceUsageService
    {
        IList<DeviceUsage> _DeviceUsage;
        IList<Building> _Buildings;

        [TestInitialize]
        public async Task TestSetup()
        {
            _DeviceUsage = await new DeviceUsageService().GetDeviceUsagesAsync();
            _Buildings = await new BuildingService().GetBuildingsAsync();
        }

        [TestMethod]
        public void Test_GetDeviceUsages()
        {
            Assert.IsNotNull(_DeviceUsage);
        }

        [TestMethod]
        public void Test_TotalPurchaseCostPerDevice()
        {
            var manufacturer = "samsung";

            var totalCost = _DeviceUsage.Where(x => x.Manufacturer.ToLower() == manufacturer.ToLower())
                .Sum(x => x.UsageStatistics.SessionInfos.Sum(y => y.Purchases.Sum(z => z.Cost)));

            Assert.IsTrue(totalCost > 0);
        }

        [TestMethod]
        public void Test_CountPurchaseByItemId()
        {
            var itemId = 47;

            var count = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         from purchase in session.Purchases
                         where purchase.ItemId == itemId
                         select item).Count();

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void Test_CountPurchaseByItemCategoryId()
        {
            var itemCategoryId = 7;

            var count = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         from purchase in session.Purchases
                         where purchase.ItemCategoryId == itemCategoryId
                         select item).Count();

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void Test_TotalPurchaseCostByState()
        {
            var state = "Ontario";

            var buildingIds = from building in _Buildings
                            where building.State.ToLower() == state.ToLower()
                            select building.BuildingId;

            var items = (from item in _DeviceUsage
                        from session in item.UsageStatistics.SessionInfos
                        where (from building in _Buildings where building.State.ToLower() == state.ToLower() select building.BuildingId)
                        .Contains(session.BuildingId)
                        select session.Purchases);

            var totalCost = items.Sum(i => i.Sum(j => j.Cost));

            Assert.IsTrue(totalCost > 0);
        }

        [TestMethod]
        public void Test_TotalPurchaseCostByCountry()
        {
            var country = "United States";

            var buildingIds = from building in _Buildings
                            where building.Country.ToLower() == country.ToLower()
                            select building.BuildingId;

            var items = (from item in _DeviceUsage
                         from session in item.UsageStatistics.SessionInfos
                         where (from building in _Buildings where building.Country.ToLower() == country.ToLower() select building.BuildingId)
                         .Contains(session.BuildingId)
                         select session.Purchases);

            var totalCost = items.Sum(i => i.Sum(j => j.Cost));

            Assert.IsTrue(totalCost > 0);
        }
        
        [TestMethod]
        public void Test_GetBuildingWithMostPurchaseCost()
        {
            var items = from item in _DeviceUsage
                        from session in item.UsageStatistics.SessionInfos
                        group session.Purchases.Sum(x => x.Cost) by session.BuildingId into p
                        select new { BuildingId = p.Key, TotalCost = p.Sum() };

            var mostPurchaseCostBuildingId = items.OrderByDescending(x => x.TotalCost).First().BuildingId;

            Dictionary<int, double> dictBuildingId2TotalPurchaseCost = new Dictionary<int, double>();
            foreach (var item in _DeviceUsage)
            {
                foreach (var session in item.UsageStatistics.SessionInfos)
                {
                    foreach (var purchase in session.Purchases)
                    {
                        if(dictBuildingId2TotalPurchaseCost.ContainsKey(session.BuildingId))
                        {
                            dictBuildingId2TotalPurchaseCost[session.BuildingId] += session.Purchases.Sum(x => x.Cost);
                        }
                        else
                        {
                            dictBuildingId2TotalPurchaseCost[session.BuildingId] = session.Purchases.Sum(x => x.Cost);
                        }
                    }
                }
            }
            
            //var b = from entry in dictBuildingId2TotalPurchaseCost orderby entry.Value descending select entry;

            var buildingId = dictBuildingId2TotalPurchaseCost.Where(x => x.Value == dictBuildingId2TotalPurchaseCost.Max(y => y.Value)).First().Key;

            Assert.AreEqual<double>(mostPurchaseCostBuildingId, buildingId);
        }
    }
}
