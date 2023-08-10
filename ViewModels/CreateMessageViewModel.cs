using ChatRooms.Models;
using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class CreateMessageViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Content { get; set; }

        public int MsgLength { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SendDate { get; set; }

        public string? UserId { get; set; }

        public int ChatroomId { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
