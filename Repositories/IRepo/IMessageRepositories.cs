using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

public interface IMessageRepository
{
    Task<List<Message>> GetMessages();
    Task<Message> GetMessageById(int id);
    Task AddMessage(Message message);
    Task UpdateMessage(Message message);
    Task RemoveMessage(int id);
}
