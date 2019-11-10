using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alexa.Models
{
    public class AlexaResponse
    {
        public string Version { get; set; }
        public ResponseBody Response { get; set; }
    }

    public class ResponseBody
    {
        [JsonProperty("outputSpeech")]
        public OutputSpeech OutputSpeech { get; set; }

        [JsonProperty("shouldEndSession", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShouldEndSession { get; set; }
    }

    public class OutputSpeech
    {

        [JsonProperty("type")]
        [JsonRequired]
        public string Type { get; set; }
        [JsonProperty("text")]
        [JsonRequired]
        public string Text { get; set; }
    }
}
