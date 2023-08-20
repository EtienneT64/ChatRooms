namespace ChatRooms.ViewModels
{
    public class ChatroomDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MsgLengthLimit { get; set; }
        public string? ChatroomImageUrl { get; set; }
        public string? OwnerUserName { get; set; }
        public string OwnerId { get; set; }
        public IFormFile? Image { get; set; }

    }
}
