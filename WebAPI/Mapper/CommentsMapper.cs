using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Mapper
{
    public static class CommentsMapper
    {
        public static CommentsDto ToCommentsDTo(this Comments comments)
        {
            return new CommentsDto
            {
                Title = comments.Title,
                Content = comments.Content,
                Created = comments.Created,
                Username = comments.AppUser?.UserName ?? string.Empty
            };
        }

        public static Comments ToComment(this CreateCommentDto dto)
        {
            return new Comments
            {
                Title = dto.Title,
                Content = dto.Content,
                Created = dto.Created,
                StockID = dto.StockID,
            };
        }

        public static void UpdateComment(this Comments comment, CommentsDto commentsDto)
        {
            comment.Title = commentsDto.Title;
            comment.Content = commentsDto.Content;
        }
    }
}
