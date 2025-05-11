using System.ComponentModel.DataAnnotations;

namespace FirsfMVC.Areas.Employee.Models
{
    public class EmployeesViewModel
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? email { get; set; }
        public string? phone { get; set; }
    }
}
