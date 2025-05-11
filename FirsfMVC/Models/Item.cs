using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirsfMVC.Models
{
    public class Item
    {
        [Key]
        public int id { get; set; }
        [Required]
        public required string name { get; set; }
        [Required]
        public int price { get; set; }

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ClientFile { get; set; }

        [Required]
        [DisplayName("Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public  Category? Category { get; set; }
    }
}
