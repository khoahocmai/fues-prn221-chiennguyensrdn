using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetComments();
        Task<Comment> GetCommentById(int id);
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task RemoveComment(int id);
    }
}