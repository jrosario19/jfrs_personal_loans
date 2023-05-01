using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class UserInforResetPasswordEmail
    {
        [Required]
        public string Email { get; set; }
    }
}
