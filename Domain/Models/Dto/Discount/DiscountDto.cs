using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.Discount
{
    public class DiscountDto : BaseDto
    {
        public int DiscountID { get; set; }
        public int Status { get; set; }
        public string DiscountName { get; set; } = string.Empty;
    }
}
