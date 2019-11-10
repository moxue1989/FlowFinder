using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowFinder.Services;
using FlowFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Humanizer;

namespace FlowFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly RouteService routeService;
        private readonly CacheService cacheService;
        public RouteController(RouteService routeService, CacheService cacheService)
        {
            this.routeService = routeService;
            this.cacheService = cacheService;
        }

        // GET: api/Route
        [HttpGet]
        public IEnumerable<RouteModel> Get()
        {
            return new List<RouteModel>()
            {
                new RouteModel(){
                        Destination = new Destination(){
                            Name = "UBC",
                            DestinationName = "University Of British Columbia"
                        },
                        TransitTime = TimeSpan.FromMinutes(20).Humanize(),
                        Distance = 13.4
                    },
                 new RouteModel(){
                        Destination = new Destination(){
                            Name = "YVR",
                            DestinationName = "Vancouver Airport"
                        },
                        TransitTime = TimeSpan.FromMinutes(60).Humanize(),
                        Distance = 45.4
                    },
                 new RouteModel(){
                        Destination = new Destination(){
                            Name = "Downtown",
                            DestinationName = "Vancouver Downtown"
                        },
                        TransitTime = TimeSpan.FromMinutes(70).Humanize(),
                        Distance = 45.4
                    }
            };
        }

        // POST: api/Route
        [HttpPost]
        public List<RouteModel> Post([FromBody] RouteRequest routeRequest)
        {
            return routeService.GetRoutes(routeRequest);
        }

        // POST: api/Route
        [HttpPost]
        [Route("preset")]
        public async Task<List<RouteModel>> PostPreset([FromBody] PresetRouteRequest presetRouteRequest)
        {
            Preset preset = await cacheService.GetPreset(presetRouteRequest.Key);
            RouteRequest routeRequest = new RouteRequest()
            {
                Origin = presetRouteRequest.Origin,
                Destinations = preset.Destinations
            };
            return routeService.GetRoutes(routeRequest);
        }
    }
}
