using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{
    public class Chatroom
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }
        public int MsgLengthLimit { get; set; }
        public string? ChatroomImageUrl { get; set; }
        public string OwnerId { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
