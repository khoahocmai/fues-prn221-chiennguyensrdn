using BusinessObjects;
using Microsoft.EntityFrameworkCore;
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

        private static MessageDAO instance = null;
        public static readonly object Lock = new object();
        private MessageDAO() { }
        public static MessageDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new MessageDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<List<Message>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            return await _context.Messages.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMessage(Message message)
        {
            var existingMessage = await _context.Messages.FindAsync(message.Id);
            if (existingMessage != null)
            {
                existingMessage.SenderId = message.SenderId;
                existingMessage.ReceiverId = message.ReceiverId;
                existingMessage.Message1 = message.Message1;
                existingMessage.UpdatedAt = DateTime.Now;

                _context.Messages.Update(existingMessage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
    }
}
