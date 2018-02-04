using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IArticleRepository
    {
        IEnumerable<Article> Articles { get; }
        void Save(Article article);
        void Remove(Article article);
    }
}
