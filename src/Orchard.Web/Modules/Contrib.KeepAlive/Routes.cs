using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;

namespace Contrib.KeepAlive {
    public class Routes : IRouteProvider {

        public void GetRoutes(ICollection<RouteDescriptor> routes) {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes() {
            return new[] {
                             new RouteDescriptor {
                                                    Priority = 1,
                                                    Route = new Route(
                                                         "keepalive",
                                                         new RouteValueDictionary {
                                                                                      {"area", "Contrib.KeepAlive"},
                                                                                      {"controller", "Ping"},
                                                                                      {"action", "Index"}
                                                                                  },
                                                         new RouteValueDictionary(),
                                                         new RouteValueDictionary {
                                                                                      {"area", "Contrib.KeepAlive"}
                                                                                  },
                                                         new MvcRouteHandler())
                                                 }
                         };
        }
    }
}