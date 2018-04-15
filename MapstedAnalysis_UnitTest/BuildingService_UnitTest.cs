using MapstedAnalytics.Models;
using MapstedAnalytics.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MapstedAnalysis_UnitTest
{
    [TestClass]
    public class BuildingService_UnitTest
    {
        [TestMethod]
        public async Task Test_GetBuildings()
        {
            IList<Building> buildings = await new BuildingService().GetBuildingsAsync();

            Assert.IsNotNull(buildings);
        }
    }
}
