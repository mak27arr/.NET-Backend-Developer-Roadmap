using BlazorServerApp.Enum;
using Microsoft.AspNetCore.SignalR;


namespace BlazorServerApp.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync(nameof(Message.ReceiveMessage), user, message);
        }
    }
}
