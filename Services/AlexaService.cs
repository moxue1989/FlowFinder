using Alexa.Models;
using FlowFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowFinder.Services
{
    public class AlexaService
    {
        private readonly RouteService routeService;
        private readonly CacheService cacheService;

        public AlexaService(RouteService routeService, CacheService cacheService)
        {
            this.routeService = routeService;
            this.cacheService = cacheService;
        }
        public AlexaResponse GetResponse(AlexaRequest alexaRequest)
        {
            switch (alexaRequest.Request.Type)
            {
                case "LaunchRequest":
                    return GetPlainTextResponse("Welcome to Flow Finder, how can I help you today?");
                case "IntentRequest":
                    return ProcessIntent(alexaRequest.Request.Intent);
                case "SessionEndedRequest":
                    return GetPlainTextResponse("Thank you for using flow finder, have a nice day!", true);

                default:
                    return GetPlainTextResponse($"I dont know that request type: {alexaRequest.Request.Type}");
            }
        }

        private  AlexaResponse ProcessIntent(Intent intent)
        {
            try
            {
                switch (intent.Name)
                {
                    case "routes":
                        return GetPlainTextResponse("Here is an update on your Personal Destinations: " + GetDefaultRoutesText());
                    case "AMAZON.StopIntent":
                    case "AMAZON.CancelIntent":
                    case "AMAZON.FallbackIntent":
                    case "AMAZON.NavigateHomeIntent":
                    case "done":
                        return GetPlainTextResponse("Thank you for using flow finder, have a nice day!", true);
                    default:
                        return GetPlainTextResponse($"Intent {intent.Name} not found, try something else");
                }
            } catch (Exception e)
            {
                return GetPlainTextResponse($"Sorry something went wrong: {e.Message}");
            }
        }
        public string GetDefaultRoutesText()
        {
            RouteRequest request = new RouteRequest()
            {
                Origin = "University Of British Columbia",
                Destinations = cacheService.GetDefault().Destinations
            };
            List<RouteModel> results = routeService.GetRoutes(request);

            List<string> list = results.Select(x => x.ToString())
                .ToList();
            return string.Join(" ", list);
        }

        private static AlexaResponse GetPlainTextResponse(string text)
        {
            return GetPlainTextResponse(text, false);
        }

        private static AlexaResponse GetPlainTextResponse(string text, bool endSession)
        {
            OutputSpeech speech = new OutputSpeech()
            {
                Type = "PlainText",
                Text = text
            };

            return new AlexaResponse()
            {
                Version = "1.0",
                Response = new ResponseBody()
                {
                    OutputSpeech = speech,
                    ShouldEndSession = endSession
                },
            };
        }
    }
}
