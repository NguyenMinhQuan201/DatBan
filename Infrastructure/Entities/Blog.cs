using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Blog : BaseEntity
    {
        public int IdBlog { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public string? CreateAtBy { get; set; } // duoc tao boi ai do
        public bool Status { get; set; }

    }
}
