using System;
using System.ComponentModel.DataAnnotations;

namespace SkopeiAPI.Models
{
    // Models are used to make a one to one connection with the database tables
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}