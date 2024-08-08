using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Repository.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comments>> GetAllAsync();
        Task<CommentsDto?> GetByIdAsync(int id);
        Task Add(Comments comment);
        Task<CommentsDto?> Update(int id, CommentsDto commentsDto);
        Task<CommentsDto?> Delete(int id);
    }
}
