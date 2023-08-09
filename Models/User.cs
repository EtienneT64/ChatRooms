using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{

    public class User : IdentityUser
    {
        [Required, StringLength(30), Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required, StringLength(7)]
        public string DisplayNameColor { get; set; }

        public int? ChatroomId { get; set; }

        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chatroom>? Chatrooms { get; set; }
    }
}
