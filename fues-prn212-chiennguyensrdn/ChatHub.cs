using Microsoft.AspNetCore.SignalR;

namespace fues_prn212_chiennguyensrdn;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

}
