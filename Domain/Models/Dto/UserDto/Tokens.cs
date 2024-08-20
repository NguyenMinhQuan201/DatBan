using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dto.UserDto
{
    public class Tokens
    {
        public string? Access_Token { get; set; }
        public string? Refresh_Token { get; set; }
    }
    public class RoleAndOperation
    {
        public string? Code { get; set; }
        public Guid RoleId { get; set; }
    }
    public class EmailChecked
    {
        public string Email { get; set; }   
        public string ClientUrl { get;set; }
        public string TokenRenew { get; set; }

    }
    public class RenewPassword
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string TokenRenew { get; set; }
    }
}
