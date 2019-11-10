using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowFinder.Models
{
    public class AuthorizedDirectionRequest : DirectionsRequest
    {
        private readonly string apiKey;

        public AuthorizedDirectionRequest(string apiKey)
        {
            this.apiKey = apiKey;
        }

        protected override QueryStringParametersList GetQueryStringParameters()
        {
            QueryStringParametersList queryParams = base.GetQueryStringParameters();
            queryParams.Add("key", apiKey);
            return queryParams;
        }
    }
}
