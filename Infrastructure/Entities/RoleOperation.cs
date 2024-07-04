using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class RoleOperation : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public int OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
}
