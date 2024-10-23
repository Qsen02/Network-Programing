using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entity
{
    public class Biography:BaseEntity
    {
        public string BiographyData {  get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationally {  get; set; }
        public int AuthorId {  get; set; }
        public virtual Author Author { get; set; }
    }
}
