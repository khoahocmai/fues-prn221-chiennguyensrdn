using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public Task<List<Comment>> GetComments()
            => CommentDAO.Instance.GetComments();

        public Task<Comment> GetCommentById(int id)
            => CommentDAO.Instance.GetCommentById(id);

        public Task AddComment(Comment comment)
            => CommentDAO.Instance.AddComment(comment);

        public Task UpdateComment(Comment comment)
            => CommentDAO.Instance.UpdateComment(comment);

        public Task RemoveComment(int id)
            => CommentDAO.Instance.RemoveComment(id);
    }
}
