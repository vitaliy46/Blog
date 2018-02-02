using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    class Articles
    {
        [TestMethod]
        public void CanPaginate()
        {
            //Mock<IArticleRepository> mock = new Mock<IArticleRepository>();
            //mock.Setup(m => m.Articles).Returns(new List<Article>
            //{
            //    new Article{Id = 1, Title = "Article1"},
            //    new Article{Id = 1, Title = "Article2"},
            //    new Article{Id = 1, Title = "Article3"},
            //    new Article{Id = 1, Title = "Article4"},
            //    new Article{Id = 1, Title = "Article5"}
            //});

            //ArticlesController controller = new ArticlesController(mock.Object);
            //controller.pageSize = 3;

            //IEnumerable<Article> result = (IEnumerable<Article>)controller.Index(2);

            //List<Article> articles = result.ToList();
            //Assert.IsTrue(articles.Count == 2);
            //Assert.AreEqual(articles[0].Title, "Article1");
            //Assert.AreEqual(articles[1].Title, "Article5");
        }
    }
}
