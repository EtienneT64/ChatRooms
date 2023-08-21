﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{

    public class User : IdentityUser
    {
        [StringLength(7)]
        public string? ProfileImageUrl { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chatroom>? Chatrooms { get; set; }
        public ICollection<UserPinnedChatroom>? PinnedChatrooms { get; set; }
    }
}
