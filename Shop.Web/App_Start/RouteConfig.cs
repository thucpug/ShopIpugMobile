using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Shop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "dangki",
              url: "dangki",
              defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
          );
            routes.MapRoute(
           name: "lienhe",
           url: "lienhe",
           defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
            name: "timkiem",
            url: "timkiem",
            defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "homeview",
            url: "homeview",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "about",
            url: "tai-khoan",
            defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
             name: "account",
             url: "dang-nhap",
             defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "productcategory",
               url: "products_detail/{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "product",
             url: "product_detail/{id}",
             defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );


           
        }
    }
}
