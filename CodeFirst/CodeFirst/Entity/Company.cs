using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entity
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees {  get; set; }
    }
}
