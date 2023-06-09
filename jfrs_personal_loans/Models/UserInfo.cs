﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Models
{
    public class UserInfo
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserPhotoPath { get; set; }
    }
}
