using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Product
{
    public class RestaurantDto : BaseDto
    {
        public int RestaurantID { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
    }
    public class Rating
    {
        public int Id { get; set; }
        public int Stars { get; set; }
        public string? Name { get; set; }
        public int SDT { get; set; }
        public string? Des { get; set; }
        public DateTime DateCreate { get; set; }
        public int IdOrder { get; set; }
    }
}
