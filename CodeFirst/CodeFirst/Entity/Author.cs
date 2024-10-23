using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entity
{
    public class Author:BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get;set; }
        public int BiographyId { get; set; }

        public virtual Biography Biography { get; set; }
    }
}
