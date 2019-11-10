using FlowFinder.Models;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using Humanizer;
using Humanizer.Localisation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowFinder.Services
{
    public class RouteService
    {
        private readonly IConfiguration configuration;

        public RouteService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<RouteModel> GetRoutes(RouteRequest routeRequest)
        {
            List<RouteModel> routeModels = new List<RouteModel>();

            foreach (Destination destination in routeRequest.Destinations)
            {
                int tryCount = 0;
                while (tryCount < 3)
                {
                    try
                    {
                        RouteModel routeModel = GetRoute(routeRequest, destination);
                        routeModels.Add(routeModel);
                        break;
                    }
                    catch (Exception)
                    {
                        tryCount++;
                    }
                }
            }
            return routeModels;
        }

        private RouteModel GetRoute(RouteRequest routeRequest, Destination destination)
        {
            DirectionsResponse directionsResponse = GetDirections(routeRequest.Origin,
                                    destination.DestinationName, TravelMode.Transit);
            Leg leg = directionsResponse.Routes.FirstOrDefault().Legs.FirstOrDefault();

            double distanceInKm = leg.Distance.Value / 1000;
            RouteModel routeModel = new RouteModel()
            {
                Destination = destination,
                Distance = distanceInKm,
                TransitTime = leg.Duration.Value.Humanize(2, minUnit: TimeUnit.Minute),
                Savings = distanceInKm * 1.5,
                DirectionsLink = BuildDirectionsUri(routeRequest, destination)
            };
            return routeModel;
        }

        private static Uri BuildDirectionsUri(RouteRequest routeRequest, Destination destination)
        {
            string rawLink = $"https://www.google.ca/maps/dir/{routeRequest.Origin}/{destination.DestinationName}";
            string link = rawLink.Replace(' ', '+');
            return new Uri(link);
        }

        private DirectionsResponse GetDirections(string origin, string destination, TravelMode travelMode)
        {
            DirectionsRequest directionsRequest = new AuthorizedDirectionRequest(configuration["GoogleMaps:ApiKey"])
            {
                Origin = origin,
                Destination = destination,
                TravelMode = travelMode,
                DepartureTime = DateTime.Now
            };

            DirectionsResponse directions = GoogleMaps.Directions.QueryAsync(directionsRequest).Result;
            return directions;
        }
    }
}