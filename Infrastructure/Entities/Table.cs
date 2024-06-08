using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Table : BaseEntity
    {
        public int TableID { get; set; }
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public int AreaID { get; set; }
    }
}
