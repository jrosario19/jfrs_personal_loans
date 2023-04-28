using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserPhotoPath { get; set; }
    }
}
