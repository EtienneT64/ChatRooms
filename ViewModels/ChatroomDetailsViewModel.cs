﻿using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class ChatroomDetailsViewModel
    {
        public int Id { get; set; }
        [MinLength(2, ErrorMessage = ("Name must be at least 2 characters"))]
        [MaxLength(30, ErrorMessage = ("Name may not exceed 30 characters"))]
        public string Name { get; set; }
        [MinLength(2, ErrorMessage = ("Description must be at least 2 characters"))]
        [MaxLength(120, ErrorMessage = ("Description may not exceed 120 characters"))]
        public string Description { get; set; }
        public int MsgLengthLimit { get; set; }
        public string? ChatroomImageUrl { get; set; }
        public string? OwnerUserName { get; set; }
        public string OwnerId { get; set; }
        public IFormFile? Image { get; set; }

    }
}
