using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowFinder.Models
{
    public class Preset
    {
        public string Key { get; set; }
        public List<Destination> Destinations{ get; set; }
    }

    public class RouteRequest
    {
        public string Origin { get; set; }
        public List<Destination> Destinations { get; set; }
    }

    public class PresetRouteRequest
    {
        public string Origin { get; set; }
        public string Key { get; set; }
    }


    public class Destination
    {
        public string Name { get; set; }
        public string DestinationName { get; set; }
    }

    public class RouteModel
    {
        public Destination Destination { get; set; }
        public string TransitTime { get; set; }
        public double Distance { get; set; }
        public double Savings { get; set; }
        public Uri DirectionsLink { get; set; }

        public override string ToString()
        {
            return $"For destination: {Destination.Name}, current transit time is {TransitTime} and distance is {Math.Round(Distance, 2)} Kilometers.";
        }
    }
}
