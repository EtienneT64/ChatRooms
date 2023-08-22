using System.ComponentModel.DataAnnotations.Schema;

namespace ChatRooms.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? ProfileImageUrl { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
