using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class UserInfoResetPassword
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
