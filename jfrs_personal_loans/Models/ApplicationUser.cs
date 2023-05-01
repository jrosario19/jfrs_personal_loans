using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ResetPasswordCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResetPasswordCodeTimeStamp { get; set; }
        public string UserPhotoPath { get; set; }

    }
}
