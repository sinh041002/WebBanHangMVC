using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanhangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product",
                url: "san-pham",
                  defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "Banhangonline.Controllers" }
            );
            routes.MapRoute(
               name: "ShoppingCard",
               url: "gio-hang",
                 defaults: new { controller = "ShoppingCard", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "Banhangonline.Controllers" }
           );
            routes.MapRoute(
                name: "CategoryProduct",
                url: "san-pham/{alias}-{id}",
                  defaults: new { controller = "Products", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Banhangonline.Controllers" }
            );

            routes.MapRoute(
               name: "DetailProduct",
               url: "chi-tiet/{alias}-p{id}",
                 defaults: new { controller = "Products", action = "Details", id = UrlParameter.Optional },
               namespaces: new[] { "Banhangonline.Controllers" }
           );
            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "Banhangonline.Controllers" }
          );
            routes.MapRoute(
            name: "News",
            url: "tin-tuc",
              defaults: new { controller = "New", action = "Index", alias = UrlParameter.Optional },
            namespaces: new[] { "Banhangonline.Controllers" }
        );
            routes.MapRoute(
                       name: "Checkout",
                       url: "thanh-toan",
                         defaults: new { controller = "ShoppingCard", action = "Checkout", alias = UrlParameter.Optional },
                       namespaces: new[] { "Banhangonline.Controllers" }
                   );
            routes.MapRoute(
                       name: "CheckOutSuccess",
                       url: "ShoppingCart/CheckOutSuccess",
                         defaults: new { controller = "ShoppingCard", action = "CheckOutSuccess", alias = UrlParameter.Optional },
                       namespaces: new[] { "Banhangonline.Controllers" }
                   );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                  defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Banhangonline.Controllers" }
            );
        }
    }
}
