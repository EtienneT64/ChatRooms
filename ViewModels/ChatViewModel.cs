using ChatRooms.Models;

namespace ChatRooms.ViewModels
{
    public class ChatViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public CreateMessageViewModel CreateMessage { get; set; }
    }
}
