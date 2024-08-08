using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title minimum lenght is 2 character")]
        [MaxLength(100, ErrorMessage = "Title maximum lenght is 100 character")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "Content minimum lenght is 2 character")]
        [MaxLength(200, ErrorMessage = "Content maximum lenght is 200 character")]
        public string Content { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public int? StockID { get; set; }

    }
}
