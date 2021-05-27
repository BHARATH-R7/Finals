using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finals.Models
{
    public class Query
    {
        [Key]
        public int QueryID { get; set; }
        [Required]
        [Display(Name = "QContent")]
        public string description { get; set; }

        public string Qstate { get; set; }

        public int EmployeeID { get; set; }
        public int CategoryId { get; set; }
        public string Scontent { get; set; }
        public Employee employee { get; set; }
        public Category category { get; set; }
    }
}
