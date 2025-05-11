using System.ComponentModel.DataAnnotations;

namespace FirsfMVC.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int id { get; set; }

        [Required]
        public required string Name { get; set; }

        public virtual ICollection<Item>? Items { get; set; } = new List<Item>();
    }
}
