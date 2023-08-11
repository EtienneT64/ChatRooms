using ChatRooms.Interfaces;
using ChatRooms.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatRooms.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageRepository _messageRepository;

        public ChatHub(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public Task JoinRoom(int chatroomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, chatroomId.ToString());
        }

        public Task LeaveRoom(int chatroomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, chatroomId.ToString());
        }

        public async Task SendMessageToGroup(int chatroomId, string userId, string messageContent)
        {
            var message = new Message
            {
                Content = messageContent,
                MsgLength = messageContent.Length,
                SendDate = DateTime.Now,
                UserId = userId,
                ChatroomId = chatroomId
            };

            _messageRepository.Add(message);


            await Clients.Group(chatroomId.ToString()).SendAsync("ReceiveMessage", Context.User.Identity.Name, message);
            //await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity, message);
        }

        public async Task SendMessage(int chatroomId, string userId, string messageContent)
        {
            var message = new Message
            {
                Content = messageContent,
                MsgLength = messageContent.Length,
                SendDate = DateTime.Now,
                UserId = userId,
                ChatroomId = chatroomId
            };

            _messageRepository.Add(message);
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, messageContent);
        }
    }
}
