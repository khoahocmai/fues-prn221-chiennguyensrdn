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
            using var db = new FUESManagementContext();
            return await db.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Comments.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddComment(Comment comment)
        {
            try
            {
                using var db = new FUESManagementContext();
                await db.Comments.AddAsync(comment);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                Console.WriteLine($"Error adding comment: {ex.Message}");
                // You can also log to a file, a logging framework, or your preferred logging system
                throw; // Rethrow the exception if you want to propagate it further
            }
        }

        public async Task UpdateComment(Comment comment)
        {
            using var db = new FUESManagementContext();
            var existingComment = await db.Comments.FindAsync(comment.Id);
            if (existingComment != null)
            {
                existingComment.ProductId = comment.ProductId;
                existingComment.UserId = comment.UserId;
                existingComment.Comment1 = comment.Comment1;
                existingComment.UpdatedAt = DateTime.Now;

                db.Comments.Update(existingComment);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveComment(int id)
        {
            using var db = new FUESManagementContext();
            var comment = await db.Comments.FindAsync(id);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Comment>> GetCommentsByProductId(int productId)
        {
            using var db = new FUESManagementContext();
            return await db.Comments
                .Where(c => c.ProductId == productId)
                .OrderByDescending(c => c.CreatedAt)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
