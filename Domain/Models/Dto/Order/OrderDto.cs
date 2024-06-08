using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class OrderDto : BaseDto
    {
        public int OrderID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public double PriceTotal { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public int NumberOfCustomer { get; set; }
        public int TableID { get; set; }
        public double Payment { get; set; }
        public double VAT { get; set; }
        public int Phone { get; set; }
        public int DiscountID { get; set; }
    }
    public class ChartCol
    {
        public decimal ChartPrice { get; set; }
        public decimal Month { get; set; }
    }
    public class ChartColDay
    {
        public decimal ChartPrice { get; set; }
        public decimal Day { get; set; }
    }
    public class ChartRadius
    {
        public decimal ChartPrice { get; set; }
        public decimal Year { get; set; }
    }
}
