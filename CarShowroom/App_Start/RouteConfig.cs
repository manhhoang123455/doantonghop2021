using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarShowroom
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
           name: "chi tiet bai  viet",
           url: "chi-tiet-bai-viet/{slug}",
           defaults: new { controller = "BaivietCus", action = "postDetail", id = UrlParameter.Optional },
           new[] { "CarShowroom.Controllers" }
           );
            routes.MapRoute(
            name: "bai viet",
            url: "bai-viet",
            defaults: new { controller = "BaivietCus", action = "index", id = UrlParameter.Optional },
            new[] { "CarShowroom.Controllers" }
            );
            //can doi
            routes.MapRoute(
            name: "huy thanh toan online",
            url: "cancel-order",
            defaults: new { controller = "GioHang", action = "cancel_order", id = UrlParameter.Optional },
            new[] { "CarShowroom.Controllers" }
            );
            routes.MapRoute(
           name: "thanh toan thanh cong",
           url: "confirm-orderPaymentOnline",
           defaults: new { controller = "GioHang", action = "confirm_orderPaymentOnline", id = UrlParameter.Optional },
           new[] { "CarShowroom.Controllers" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new []{ "CarShowroom.Controllers" }
            );
        }
    }
}
