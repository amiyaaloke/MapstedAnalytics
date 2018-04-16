using MapstedAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapstedAnalytics.Service
{
    interface IDeviceUsageService
    {
        Task<IList<DeviceUsage>> GetDeviceUsagesAsync();
        double GetTotalPurchaseCostPerManufacturer(string manufacturer);
        int GetCountOfPurchaseByItemId(int itemId);
        int GetCountOfPurchaseByItemCategoryId(int itemCategoyId);
        double GetTotalPurchaseCostByState(string state);
        double GetTotalPurchaseCostByCountry(string country);
        int GetBuildingWithMostPurchaseCost();
    }
}
