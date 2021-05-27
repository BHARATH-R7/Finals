using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finals.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name = "EName")]
        public string EName { get; set; }

        [Required(ErrorMessage = "This field can't be empty")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "password doesn't match")]
        [Display(Name = "ConfirmPassword")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "It is Must!")]
        [Display(Name = "EmailID")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is Not Valid")]
        public string EMail { get; set; }

        public ICollection<Query> Queries { get; set; }
    }
}
