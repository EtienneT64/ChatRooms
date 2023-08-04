namespace ChatRooms.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int MsgLength { get; set; }
        public DateTime SendDate { get; set; }

        public User User { get; set; }
    }
}
