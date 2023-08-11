using ChatRooms.Models;

namespace ChatRooms.ViewModels
{
    public class ChatViewModel
    {
        public int ChatroomId { get; set; }
        public string ChatroomName { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public CreateMessageViewModel CreateMessage { get; set; }
    }
}
