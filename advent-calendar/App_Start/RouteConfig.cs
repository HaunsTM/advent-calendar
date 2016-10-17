using System.Web.Mvc;
using System.Web.Routing;

namespace advent_calendar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*http://jamesdmosier.com/blog/2015/09/20/angular-vs/ */
            routes.MapRoute(
                name: "API",
                url: "api/{controller}/{action}",
                defaults: new { controller = "Login", action = "Authenticate" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
