﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class Module : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string?Code { get; set; }
        public string?Name { get; set; }
        public int Order { get; set; }
        public bool IsShow { get; set; }
    }
}
