using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using Domain.Entities;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        //private EFDbContext db = new EFDbContext();

        private IArticleRepository articleRepository;
        private ICategoryRepository categoryRepository;

        public AdminController(IArticleRepository articleRepo, ICategoryRepository categoryRepo)
        {
            articleRepository = articleRepo;
            categoryRepository = categoryRepo;
        }

        // GET: Admin
        public ActionResult ArticlesIndex()
        {
            return View(articleRepository.Articles);
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);
                return View(article);
            }

            return Redirect("/Admin");
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            //ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name");

            return View();
        }

        // POST: Admin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Author,CategoryId")] Article article)
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

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);

            if(article != null)
            {
                ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);
                return View(article);
            }

            return Redirect("/Admin");
        }

        // POST: Admin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Author,CategoryId")] Article article)
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

        // GET: Admin/Delete/5
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

        // POST: Admin/Delete/5
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
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
