using MapstedAnalytics.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapstedAnalysis_UnitTest
{
    [TestClass]
    public class Test_DeviceUsageService
    {
        [TestMethod]
        public async Task Test_GetDeviceUsages()
        {
            var deviceUsages = await new DeviceUsageService().GetDeviceUsagesAsync();
        }
    }
}
