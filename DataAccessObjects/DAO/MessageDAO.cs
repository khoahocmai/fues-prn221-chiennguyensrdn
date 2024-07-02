using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class MessageDAO
    {
        private readonly FUESManagementContext _context;

        public MessageDAO(FUESManagementContext context)
        {
            _context = context;
        }

        public Message GetMessage(int id)
        {
            Message message = _context.Messages.Find(id);
            return message;
        }

        public List<Message> GetMessages()
        {
            List<Message> messages = _context.Messages.ToList();
            return messages;
        }
        public void CreateMessage(Message message)
        {
             _context.Messages.Add(message);
        }

        public void UpdateMessage(Message message)
        {
            Message currentMessage = _context.Messages.Find(message.Id);
            _context.Messages.Update(currentMessage);
        }

        public void DeleteMessage(int id)
        {
            Message message = _context.Messages.Find(id);
            _context.Messages.Remove(message);
        }
    }
}
