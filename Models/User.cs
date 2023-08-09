using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{

    public class User : IdentityUser
    {
        [StringLength(7)]
        public string? DisplayNameColor { get; set; }

        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chatroom>? Chatrooms { get; set; }
    }
}
