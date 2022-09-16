using System.ComponentModel.DataAnnotations;

namespace SkopeiAPI.Models.Dto
{
    // Dto is used for creation of objects. This abstracts things like Id from user input.
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
