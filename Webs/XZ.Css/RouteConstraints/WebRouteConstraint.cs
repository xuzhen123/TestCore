using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XZ.Css.RouteConstraints
{
    public class WebRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (RouteDirection.IncomingRequest == routeDirection)
            {

                if (!string.IsNullOrWhiteSpace(routeKey))
                {
                    var value = values[routeKey];

                    if (!string.IsNullOrWhiteSpace(value?.ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
