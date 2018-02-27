using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Articles",
                url: "",
                defaults: new { controller = "Articles", action = "Index", category = (string) null, page = 1 },
                namespaces: new[] { "WebUI.Controllers" }
            );

            routes.MapRoute(
                name: "ArticlesPage",
                url: "page{page}",
                defaults: new { controller = "Articles", action = "Index", category = (string)null, page = 1 },
                constraints: new { page = @"\d+" },
                namespaces: new[] { "WebUI.Controllers" }
            );

            routes.MapRoute(
                name: "ArticlesCategory",
                url: "{category}",
                defaults: new { controller = "Articles", action = "Index", page = 1 },
                namespaces: new[] { "WebUI.Controllers" }
            );

            routes.MapRoute(
                "ArticlesCategoryPage",
                "{category}/page{page}",
                new { controller = "Articles", action = "Index" },
                new { page = @"\d+" },
                namespaces: new[] { "WebUI.Controllers" }
            );

            routes.MapRoute(
                name: null,
                url: "{controller}/{action}",
                namespaces: new[] { "WebUI.Controllers" }
            );
        }
    }
}
