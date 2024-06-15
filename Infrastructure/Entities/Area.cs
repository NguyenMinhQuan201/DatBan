using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Area: BaseEntity
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; } = string.Empty;

        public int RestaurantID { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public ICollection<Table> Tables { get; set; }

    }
}
