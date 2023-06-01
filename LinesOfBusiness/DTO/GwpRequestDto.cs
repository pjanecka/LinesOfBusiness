using System.ComponentModel.DataAnnotations;

namespace LinesOfBusiness.DTO
{
    public class GwpRequestDto
    {
        [Required]
        [RegularExpression(@"^[A-Za-z]{2}$", ErrorMessage = "Country must contain exactly 2 characters")]
        public string Country { get; set; } = null!;

        [Required]
        [MinLength(1, ErrorMessage = "Lob must contain at least one item")]
        public List<string> Lob { get; set; } = null!;
    }
}
