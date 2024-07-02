using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class CommentDAO
    {
        private readonly FUESManagementContext _context;

        public CommentDAO(FUESManagementContext context)
        {
            _context = context;
        }

        private static CommentDAO instance = null;
        public static readonly object Lock = new object();
        private CommentDAO() { }
        public static CommentDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new CommentDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(comment.Id);
            if (existingComment != null)
            {
                existingComment.ProductId = comment.ProductId;
                existingComment.UserId = comment.UserId;
                existingComment.Comment1 = comment.Comment1;
                existingComment.UpdatedAt = DateTime.Now;

                _context.Comments.Update(existingComment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
