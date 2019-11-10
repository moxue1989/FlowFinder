using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlowFinder.Models;
using FlowFinder.Services;

namespace FlowFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RouteService routeService;
        private readonly CacheService cacheService;

        public HomeController(ILogger<HomeController> logger, RouteService routeService, CacheService cacheService)
        {
            _logger = logger;
            this.routeService = routeService;
            this.cacheService = cacheService;
        }

        public IActionResult Index(string key = "default", string origin = "UBC")
        {
            Preset preset = cacheService.GetPreset(key).Result;
            List<RouteModel> routeModels = routeService.GetRoutes(new RouteRequest()
            {
                Origin = origin,
                Destinations = preset.Destinations
            });
            ViewData["Key"] = key;
            return View(routeModels);
        }

        public IActionResult Preset()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
