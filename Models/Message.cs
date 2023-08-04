using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }


        public int UserId { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;


        public int MsgLength { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SendDate { get; set; }

        public User User { get; set; }
        public Chatroom Chatroom { get; set; }
    }
}
