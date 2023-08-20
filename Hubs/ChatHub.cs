using ChatRooms.Helpers;
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
        private readonly IChatroomRepository _chatroomRepository;

        public ChatHub(IMessageRepository messageRepository, IChatroomRepository chatroomRepository)
        {
            _messageRepository = messageRepository;
            _chatroomRepository = chatroomRepository;
        }
        public async Task JoinRoom(string chatroomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatroomName);

            await Clients.Group(chatroomName).SendAsync("Send", $"{Context.User.Identity.Name} has joined the group {chatroomName}.");
        }

        public async Task LeaveRoom(string chatroomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatroomName);

            await Clients.Group(chatroomName).SendAsync("Send", $"{Context.User.Identity.Name} has left the group {chatroomName}.");
        }


        public async Task SendMessageToGroup(string chatroomName, string userId, string messageContent)
        {
            var chatroom = await _chatroomRepository.GetByNameAsync(chatroomName);
            var newMessage = new Message
            {
                Content = messageContent,
                Length = messageContent.Length,
                TimeStamp = DateTime.Now,
                UserId = userId,
                ChatroomId = chatroom.Id,
            };

            _messageRepository.Add(newMessage);

            string messageTimeStamp = FormatTime.FormatTimeStamp(newMessage.TimeStamp, DateTime.Now);
            await Clients.Group(chatroomName).SendAsync("ReceiveMessage", Context.User.Identity.Name, messageTimeStamp, messageContent);
        }
    }
}
