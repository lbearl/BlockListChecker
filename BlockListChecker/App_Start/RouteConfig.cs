using System.Web.Mvc;
using System.Web.Routing;

namespace BlockListChecker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Enable MVC attribute routing.
            routes.MapMvcAttributeRoutes();
        }
    }
}
