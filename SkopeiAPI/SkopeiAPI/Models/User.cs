using System;
using System.ComponentModel.DataAnnotations;

namespace SkopeiAPI.Models
{
    // Models are used to make a one to one connection with the database tables
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [Required]
        public bool IsDeleted { get; set; }
    }
}