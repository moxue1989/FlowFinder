using FlowFinder.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FlowFinder.Services
{
    public class CacheService
    {
        private readonly IDistributedCache distCache;
        private const string PresetField = "preset_";
        private const string Default = "default";

        public CacheService(IDistributedCache cache)
        {
            distCache = cache;
        }

        public Preset GetDefault()
        {
            return GetPreset(Default).Result;
        }

        public async Task<string> SavePreset(Preset preset)
        {
            await distCache.SetStringAsync(PresetField + preset.Key, JsonConvert.SerializeObject(preset));
            return preset.Key;
        }

        public async Task<Preset> GetPreset(string key)
        {
            string presetJson = await distCache.GetStringAsync(PresetField + key);
            return string.IsNullOrEmpty(presetJson) ? null 
                : JsonConvert.DeserializeObject<Preset>(presetJson);            
        }
    }
}
