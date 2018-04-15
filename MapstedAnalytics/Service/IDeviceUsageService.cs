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
    }
}
