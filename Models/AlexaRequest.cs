using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.Models
{
    public class AlexaRequest
    {
        [JsonProperty("request")]
        public Request Request { get; set; }

    }

    public class Request
    {
        [JsonProperty("type", Required = Required.Always)]
        public string Type { get; set; }

        public Intent Intent { get; set; }
    }

    public class Intent
    {
        public string Name { get; set; }
    }
}
