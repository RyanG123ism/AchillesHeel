using AchillesHeel_RG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AchillesHeel_RG
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //stripe success route
            routes.MapRoute(
                name: "thanks",
                url: "thank-you-for-your-payment",
                defaults: new { controller = "Checkout", action = "Success", id = UrlParameter.Optional }
            );

            //stripe unsuccessful route
            routes.MapRoute(
                name: "cancel",
                url: "order-cancelled",
                defaults: new { controller = "Checkout", action = "Cancelled", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
