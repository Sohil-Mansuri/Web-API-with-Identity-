using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.DTO;
using WebAPI.Mapper;
using WebAPI.Models;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository.Implemetation
{
    public class CommentRespsitory : ICommentRepository
    {
        private readonly SohilTestContext _context;
        public CommentRespsitory(SohilTestContext context)
        {
            _context = context;
        }

        public async Task Add(Comments comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync(); 
        }

        public async Task<CommentsDto?> Delete(int id)
        {
           var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return null;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment.ToCommentsDTo();
        }

        public async Task<List<Comments>> GetAllAsync()
        {
            return await _context.Comments.Include(c => c.AppUser).ToListAsync();
        }

        public async Task<CommentsDto?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(c => c.AppUser).FirstOrDefaultAsync(c => c.ID == id);
            if (comment is null) return null;
            return comment.ToCommentsDTo();
        }

        public async Task<CommentsDto?> Update(int id, CommentsDto commentsDto)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment is null) return null;
            comment.UpdateComment(commentsDto);  
            await _context.SaveChangesAsync();
            return comment.ToCommentsDTo();
        }

    }
}
