using ChatRooms.Models;

namespace ChatRooms.ViewModels
{
    public class CreateMessageViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int MsgLength { get; set; }

        public DateTime SendDate { get; set; }

        public string UserId { get; set; }

        public int ChatroomId { get; set; }

        public IEnumerable<Message>? Messages { get; set; }

    }
}
