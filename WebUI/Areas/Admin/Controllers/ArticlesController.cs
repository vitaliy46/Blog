using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    public class ArticlesController : Controller
    {
        private IArticleRepository articleRepository;
        private ICategoryRepository categoryRepository;
        public int pageSize = 10;

        public ArticlesController(IArticleRepository articleRepo, ICategoryRepository categoryRepo)
        {
            articleRepository = articleRepo;
            categoryRepository = categoryRepo;
        }
        // GET: Admin/Articles
        public ActionResult Index(int page = 1)
        {
            ArticlesIndexViewModel model = new ArticlesIndexViewModel
            {
                Articles = articleRepository.Articles
                    .OrderBy(article => article.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = articleRepository.Articles.Count()
                },
                CurrentCategory = null
            };

            return View(model);
        }

        // GET: Admin/Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id != null)
            {
                Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);
                return View(article);
            }

            return Redirect("/Admin");
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name");

            return View();
        }

        // POST: Admin/Articles/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ShortText,Text,Author,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                articleRepository.Save(article);
                TempData["message"] = string.Format("Статья \"{0}\" сохранена", article.Title);
                return Redirect("/Admin");
            }

            ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);

            return View(article);
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);
                return View(article);
            }

            return Redirect("/Admin");
        }

        // POST: Admin/Articles/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ShortText,Text,Author,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                articleRepository.Save(article);
                TempData["message"] = string.Format("Изменения \"{0}\" сохранены", article.Title);
                return Redirect("/Admin");
            }

            ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);

            return View(article);
        }

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);
                return View(article);
            }

            return Redirect("/Admin");
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                articleRepository.Remove(article);
                TempData["message"] = "Статья удалена";
                return Redirect("/Admin");
            }

            return Redirect("/Admin");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
