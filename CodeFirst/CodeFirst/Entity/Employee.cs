using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entity
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company {  get; set; }
    }
}
