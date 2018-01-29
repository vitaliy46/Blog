using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Concrete
{
    public class EFArticleRepository : IArticleRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Article> Articles
        {
            get { return context.Articles; }
        }
    }
}
