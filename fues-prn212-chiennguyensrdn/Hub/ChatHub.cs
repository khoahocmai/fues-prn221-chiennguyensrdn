using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessObjects;
using BusinessObjects;
using System.Security.Claims;

public class ChatHub : Hub
{
    private readonly FUESManagementContext _context;
    // Dictionary to hold user connections in memory
    private static readonly Dictionary<int, string> _userConnections = new();

    public ChatHub(FUESManagementContext context)
    {
        _context = context;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserIdFromClaims(Context.User);
        if (userId != null)
        {
            _userConnections[userId.Value] = Context.ConnectionId;
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = GetUserIdFromClaims(Context.User);
        if (userId != null)
        {
            _userConnections.Remove(userId.Value);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessageToUser(int receiverUserId, string message)
    {
        var senderUserId = GetUserIdFromClaims(Context.User);
        if (senderUserId != null)
        {
            var chatMessage = new Message
            {
                SenderId = senderUserId,
                ReceiverId = receiverUserId,
                Message1 = message,
                CreatedAt = DateTime.UtcNow
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();
            var senderName = _context.Users.Find(senderUserId).Name; 
            await Clients.Client(_userConnections[receiverUserId]).SendAsync("ReceiveMessage", senderName, message);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "System", $"User {receiverUserId} is not online.");
        }
    }

    private int? GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "UserId");
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }
        return null;
    }

}