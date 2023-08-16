namespace ChatRooms.ViewModels
{
    public class EditUserDashboardViewModel
    {
        public string Id { get; set; }
        public string? DisplayNameColor { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
