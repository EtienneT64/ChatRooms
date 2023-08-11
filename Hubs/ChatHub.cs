using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRooms.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public Task JoinRoom(string chatroomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatroomName);
        }

        public Task LeaveRoom(string chatroomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatroomName);
        }

        public async Task SendMessageToGroup(string chatroomName, string message)
        {
            await Clients.Group(chatroomName).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
