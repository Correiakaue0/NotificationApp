using Microsoft.AspNetCore.SignalR;

namespace NotificationApp.SignalR
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message, string type)
        {
            await Clients.All.SendAsync("ReceiveMessage", message, type);
        }
    }
}