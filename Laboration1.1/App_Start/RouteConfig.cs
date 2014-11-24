using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Laboration1._1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            routes.MapRoute(
                            name: "SecretNumber",
                            url: "{controller}/{action}/{id}",
                            defaults: new { controller = "SecretNumber", action = "Index", id = UrlParameter.Optional }
                        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SecretNumber", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
