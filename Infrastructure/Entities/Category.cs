using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Category: BaseEntity
    {
        public int IdCategory { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
       
        public bool Status { get; set; }
        public ICollection<Dish> Dishs { get; set; } 

    }
}
