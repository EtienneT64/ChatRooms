﻿namespace ChatRooms.ViewModels
{
    public class UserIndexViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }

        public int? OwnedChatrooms { get; set; }
        public int? PinnedChatrooms { get; set; }
        public int? UserMessages { get; set; }
    }
}
