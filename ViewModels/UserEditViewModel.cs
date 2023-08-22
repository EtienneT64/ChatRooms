using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
