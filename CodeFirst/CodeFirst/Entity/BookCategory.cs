using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entity
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
