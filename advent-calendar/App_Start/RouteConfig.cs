﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace advent_calendar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{*.*}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}
