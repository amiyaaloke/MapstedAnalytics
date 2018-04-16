using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MapstedAnalytics.Models;
using MapstedAnalytics.Service;

namespace MapstedAnalytics.Controllers
{
    public class HomeController : Controller
    {
        [BindProperty]
        public double TotalCostSamsungDevices { get; set; }

        public async Task<IActionResult> Index()
        {
            List<Result> results = new List<Result>();
            // Total purchase costs for Samsung manufacture devices
            IDeviceUsageService deviceUsageService = new DeviceUsageService();
            await deviceUsageService.GetDeviceUsagesAsync();

            results.Add(new Result
            {
                Title = "Total purchase costs for Samsung manufacture devices",
                Value = deviceUsageService.GetTotalPurchaseCostPerManufacturer("samsung")
            });

            results.Add(new Result
            {
                Title = "Total number of times item (item_id = 47) was purchased",
                Value = deviceUsageService.GetCountOfPurchaseByItemId(47)
            });

            results.Add(new Result
            {
                Title = "Total purchase costs for item’s in the category (item_category_id = 7)",
                Value = deviceUsageService.GetCountOfPurchaseByItemCategoryId(7)
            });

            results.Add(new Result
            {
                Title = "Total purchase costs in Ontario",
                Value = deviceUsageService.GetTotalPurchaseCostByState("Ontario")
            });

            results.Add(new Result
            {
                Title = "Total purchase costs in the United States",
                Value = Math.Round(deviceUsageService.GetTotalPurchaseCostByCountry("United States"), 2)
            });

            results.Add(new Result
            {
                Title = "Which building (name or id) has the most total purchase costs?",
                Value = deviceUsageService.GetBuildingWithMostPurchaseCost()
            });

            ViewBag.Results = results;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
