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
        private EFDbContext db = new EFDbContext();

        private IArticleRepository articleRepository;
        private ICategoryRepository categoryRepository;

        public AdminController(IArticleRepository articleRepositoryParam, ICategoryRepository categoryRepositoryParam)
        {
            articleRepository = articleRepositoryParam;
            categoryRepository = categoryRepositoryParam;
        }

        // GET: Admin
        public ViewResult ArticlesIndex()
        {
            return View(articleRepository.Articles);
        }

        // GET: Admin/Details/5
        public ViewResult Details(int? id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);

            return View(article);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Text,Author,CategoryId")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", article.CategoryId);
            return View(article);
        }

        // GET: Admin/Edit/5
        public ViewResult Edit(int? id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);
            ViewBag.Category = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);

            return View(article);
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
                TempData["message"] = string.Format("Изменение информации о книге \"{0}\" сохранены", article.Title);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(categoryRepository.Categories, "Id", "Name", article.CategoryId);

            return View(article);
        }

        // GET: Admin/Delete/5
        public ViewResult Delete(int? id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);
            
            return View(article);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = articleRepository.Articles.FirstOrDefault(a => a.Id == id);
            articleRepository.Remove(article);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
