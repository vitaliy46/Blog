using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private ICategoryRepository repository;

        public NavigationController(ICategoryRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            return PartialView(repository.Categories);
        }
    }
}