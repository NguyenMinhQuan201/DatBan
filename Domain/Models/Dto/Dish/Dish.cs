using Domain.Models.Dto.ImportInvoice;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.ImportInvoiceDto
{
    public class DishDto : BaseDto
    {
        public int DishId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public string NameCate { get; set; }
    }
}
