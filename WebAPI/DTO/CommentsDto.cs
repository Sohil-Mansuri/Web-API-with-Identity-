using WebAPI.Models;

namespace WebAPI.DTO
{
    public class CommentsDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

        public string Username { get; set; } = string.Empty;

    }
}
