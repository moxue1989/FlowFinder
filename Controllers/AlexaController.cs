using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.Models;
using FlowFinder.Models;
using FlowFinder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlexaController : ControllerBase
    {
        private readonly AlexaService alexaService;

        public AlexaController(AlexaService alexaService)
        {
            this.alexaService = alexaService;
        }

        [HttpPost]
        public AlexaResponse Post([FromBody] AlexaRequest request)
        {
            return alexaService.GetResponse(request);
        }
    }
}