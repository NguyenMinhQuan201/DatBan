using Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features.Supplier.Dto
{
    public class TableDto : BaseDto
    {
        public int TableID { get; set; }
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public int AreaID { get; set; }
    }
    public class GetSupplierWithConvertDate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Phone { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsEnable { get; set; }
    }
}
