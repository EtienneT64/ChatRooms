using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRooms.Models
{
    public class ChatroomOwner
    {
        [ForeignKey("Chatroom")]
        public int ChatroomId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
