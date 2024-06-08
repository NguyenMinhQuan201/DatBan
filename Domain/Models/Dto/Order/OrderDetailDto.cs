using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Order
{
    public class OrderDetailDto : BaseDto
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DishID { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfCustomer { get; set; }
        public int TableID { get; set; }
        public double Payment { get; set; }
        public int Id { get; set; }
    }
    public class OrderDetailRequest
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public decimal Price { get; set; }
        public decimal Discounnt { get; set; }
        public int Quantity { get; set; }
    }
}
