using System.ComponentModel.DataAnnotations;

namespace SkopeiAPI.Models.Dto
{
    // Dto is used for creation of objects. This abstracts things like Id from user input.
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}