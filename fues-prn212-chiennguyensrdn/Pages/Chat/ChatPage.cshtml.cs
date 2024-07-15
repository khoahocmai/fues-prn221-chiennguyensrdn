using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace fues_prn221_chiennguyensrdn.Pages.Chat
{
    public class ChatPageModel : PageModel
    {
        private readonly FUESManagementContext _context;

        public ChatPageModel(FUESManagementContext context)
        {
            _context = context;
        }

        public List<User> ChatPartners { get; set; } = new List<User>();
        public List<Message> Messages { get; set; } = new List<Message>();
        public string CurrentUserName { get; set; }
        public int CurrentUserId { get; set; }
        public int ReceiverId { get; set; }

        public async Task OnGetAsync(int receiverId)
        {
            ReceiverId = receiverId;
            CurrentUserName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
            var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == CurrentUserName);

            if (currentUser != null)
            {
                CurrentUserId = currentUser.Id;
                var chatPartnerRole = currentUser.Role == "Seller" ? "Buyer" : "Seller";

                // Get chat partners who have existing messages with the current user
                var chatPartnersWithMessages = await _context.Messages
                    .Where(m => (m.SenderId == CurrentUserId && m.Receiver.Role == chatPartnerRole) ||
                                (m.ReceiverId == CurrentUserId && m.Sender.Role == chatPartnerRole))
                    .Select(m => m.SenderId == CurrentUserId ? m.ReceiverId : m.SenderId)
                    .Distinct()
                    .ToListAsync();

                // Fetch messages for the current user and receiver
                Messages = await _context.Messages
                    .Where(m => (m.SenderId == CurrentUserId && m.ReceiverId == receiverId) ||
                                (m.SenderId == receiverId && m.ReceiverId == CurrentUserId))
                    .OrderBy(m => m.CreatedAt)
                    .Include(m => m.Sender)
                    .ToListAsync();
                // Filter users based on chat partners with messages
                ChatPartners = await _context.Users
                    .Where(u => chatPartnersWithMessages.Contains(u.Id) && u.Role == chatPartnerRole)
                    .ToListAsync();

                // If no chat partners with messages, fallback to the current receiverId
                if (ChatPartners.Count == 0)
                {
                    var currentChatPartner = await _context.Users.FindAsync(receiverId);
                    if (currentChatPartner != null && currentChatPartner.Role == chatPartnerRole)
                    {
                        ChatPartners.Add(currentChatPartner);
                    }
                }
            }
        }
    }
}
