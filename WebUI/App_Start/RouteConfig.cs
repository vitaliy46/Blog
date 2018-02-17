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
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "ArticlesIndex" }
            );

            routes.MapRoute(
                name: "AdminArticleDetails",
                url: "Admin/Details/{id}",
                defaults: new { controller = "Admin", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminArticleCreate",
                url: "Admin/Create",
                defaults: new { controller = "Admin", action = "Create" }
            );

            routes.MapRoute(
                name: "AdminArticleEdit",
                url: "Admin/Edit/{id}",
                defaults: new { controller = "Admin", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AdminArticleDelete",
                url: "Admin/Delete/{id}",
                defaults: new { controller = "Admin", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Articles",
                url: "",
                defaults: new { controller = "Articles", action = "Index", category = (string) null, page = 1 }
            );

            routes.MapRoute(
                name: "ArticlesPage",
                url: "page{page}",
                defaults: new { controller = "Articles", action = "Index", category = (string)null, page = 1 },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "ArticlesCategory",
                url: "{category}",
                defaults: new { controller = "Articles", action = "Index", page = 1 }
            );

            routes.MapRoute(
                "ArticlesCategoryPage",
                "{category}/page{page}",
                new { controller = "Articles", action = "Index" },
                new { page = @"\d+" }
            );

            routes.MapRoute(
                null,
                "{controller}/{action}"
            );

            

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Articles", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
