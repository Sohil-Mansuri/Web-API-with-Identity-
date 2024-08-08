using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models;

namespace WebAPI.DTO
{
    public class CreateStockRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Symbol min lenght is 2 character")]
        [MaxLength(10, ErrorMessage = "Symbol maximum length is 10 character")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Company name minimun lenght is 5 character")]
        [MaxLength(20, ErrorMessage = "Company name maximum length is 20 character")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(100, 4000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000)]
        public long MarketCap { get; set; }

    }
}
