using Microsoft.AspNetCore.Identity;

namespace ChatRooms.Models
{

    public class User : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chatroom>? Chatrooms { get; set; }
        public ICollection<UserPinnedChatroom>? PinnedChatrooms { get; set; }
    }
}
