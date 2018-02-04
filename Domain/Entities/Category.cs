using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Category : IEnumerable
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название категории")]
        public string Name { get; set; }

        [Display(Name = "URL адрес")]
        [Required(ErrorMessage = "Пожалуйста, введите URL адрес категории")]
        public string UrlName { get; set; }

        public IList<Article> Articles { get; set; }

        public IEnumerator GetEnumerator()
        {
            return Articles.GetEnumerator();
        }
    }
}
