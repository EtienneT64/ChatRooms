using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class ChatroomCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Message Length Character Limit")]
        public int MsgLengthLimit { get; set; }
        public string? ChatroomImageUrl { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
