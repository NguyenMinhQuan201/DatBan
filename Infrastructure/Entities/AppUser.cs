﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string?FirstName { get; set; }

        public string?LastName { get; set; }

        public DateTime Dob { get; set; }
        public string? RefreshToken { get; set; }
        public int RestaurantID { get; set; }
        public int Status {  get; set; }
    }
}
