﻿using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class AccountLoginViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}