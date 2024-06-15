using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Restaurant : BaseEntity
    {
        public int RestaurantID { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
        public ICollection<Area> Areas { get; set; }

    }
}
