using Domain.Abstract;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ArticlesController : Controller
    {
        private IArticleRepository repository;
        public int pageSize = 4;

        public ArticlesController(IArticleRepository repo)
        {
            repository = repo;
        }

        // GET: Articles
        public ActionResult Index(string category, int page = 1)
        {
            ArticlesIndexViewModel model = new ArticlesIndexViewModel
            {
                Articles = repository.Articles
                    .Where(article => category == null || article.Category.UrlName == category)
                    .OrderBy(article => article.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? 
                        repository.Articles.Count() : 
                        repository.Articles.Where(article => article.Category.UrlName == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        // GET: Articles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Articles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Articles/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
