using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderID { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DishID { get; set; }
        //public DateTime Date { get; set;}
        public int NumberOfCustomer { get; set; }
        public int Quantity {  get; set; }
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        
    }
}
