using jfrs_personal_loans.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public class JFRSPersonalLoansDBContext: IdentityDbContext<ApplicationUser>
    {
        public JFRSPersonalLoansDBContext(DbContextOptions<JFRSPersonalLoansDBContext> options)
            : base(options)
        {
        }
    }
}
