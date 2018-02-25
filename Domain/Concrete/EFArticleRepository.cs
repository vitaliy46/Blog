using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFArticleRepository : IArticleRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Article> Articles
        {
            get { return context.Articles.Include(article => article.Category); }
        }

        public void Save(Article article)
        {
            if (article.Id == 0)
            {
                context.Articles.Add(article);
            }
            else
            {
                Article dbEntry = context.Articles.Find(article.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = article.Title;
                    dbEntry.ShortText = article.ShortText;
                    dbEntry.Text = article.Text;
                    dbEntry.Author = article.Author;
                    dbEntry.CategoryId = article.CategoryId;
                }
            }
            context.SaveChanges();
        }

        public void Remove(Article article)
        {
            context.Articles.Remove(article);
            context.SaveChanges();
        }
    }
}
