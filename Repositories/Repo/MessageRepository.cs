using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public Task<List<Message>> GetMessages()
            => MessageDAO.Instance.GetMessages();

        public Task<Message> GetMessageById(int id)
            => MessageDAO.Instance.GetMessageById(id);

        public Task AddMessage(Message message)
            => MessageDAO.Instance.AddMessage(message);

        public Task UpdateMessage(Message message)
            => MessageDAO.Instance.UpdateMessage(message);

        public Task RemoveMessage(int id)
            => MessageDAO.Instance.RemoveMessage(id);
    }
}
