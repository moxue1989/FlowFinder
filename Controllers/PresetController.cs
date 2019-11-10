using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowFinder.Models;
using FlowFinder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresetController : ControllerBase
    {
        private readonly CacheService cacheService;
        public PresetController(CacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        [HttpGet]
        public async Task<Preset> Get(string key)
        {
            return await cacheService.GetPreset(key);
        }

        [HttpPost]
        public async Task<string> Post(Preset preset)
        {
            string key = await cacheService.SavePreset(preset);
            return key;
        }
    }
}