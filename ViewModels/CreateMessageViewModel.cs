using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class CreateMessageViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public int MsgLength { get; set; }

        public DateTime SendDate { get; set; }

        public string UserId { get; set; }

        public int ChatroomId { get; set; }

    }
}
