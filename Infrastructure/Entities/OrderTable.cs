using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class OrderTable:BaseEntity
    {
        public int TableID { get; set; }
        public virtual Table Table { get; set; } = new Table();
        public int OrderID { get; set; }
        public virtual Order Order { get; set; } = new Order();
        public DateTime DateCreated { get; set; }
    }
}
