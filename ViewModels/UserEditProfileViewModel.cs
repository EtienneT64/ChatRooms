namespace ChatRooms.ViewModels
{
    public class UserEditProfileViewModel
    {
        public string? Id { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
