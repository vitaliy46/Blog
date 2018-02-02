using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : IEnumerable
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlName { get; set; }

        public IList<Article> Articles { get; set; }

        public IEnumerator GetEnumerator()
        {
            return Articles.GetEnumerator();
        }
    }
}
