using System.Web.Mvc;

namespace WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: null,
                url: "Admin/page{page}",
                defaults: new { controller = "Articles", action = "Index", page = 1 },
                constraints: new { page = @"\d+" }
            );

            context.MapRoute(
                name: null,
                url: "Admin",
                defaults: new { controller = "Articles", action = "Index" }
            );

            context.MapRoute(
                name: null,
                url: "Admin/Details/{id}",
                defaults: new { controller = "Articles", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: null,
                url: "Admin/Create",
                defaults: new { controller = "Articles", action = "Create" }
            );

            context.MapRoute(
                name: null,
                url: "Admin/Edit/{id}",
                defaults: new { controller = "Articles", action = "Edit", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: null,
                url: "Admin/Delete/{id}",
                defaults: new { controller = "Articles", action = "Delete", id = UrlParameter.Optional }
            );

            //context.MapRoute(
            //    "Admin_default",
            //    "Admin/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}