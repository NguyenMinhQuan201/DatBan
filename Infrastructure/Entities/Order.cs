using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Order : BaseEntity
    {
        public int OrderID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public double PriceTotal { get; set; }
        public string Description { get; set; } = string.Empty;
        //public DateTime DateCreated { get; set; }
        public int NumberOfCustomer { get; set; }
        public int TableID { get; set; }
        public double Payment { get; set; }
        public double VAT { get; set; }
        public int Phone { get; set; }
        public int DiscountID { get; set; }
        public ICollection<OrderTable> OrderTables { get; set; } = new List<OrderTable>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    }
}
