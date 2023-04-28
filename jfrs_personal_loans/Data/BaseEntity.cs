using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace jfrs_personal_loans.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedByUser { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreatedOnDate { get; set; }
        public bool IsActive { get; set; }
    }
}
