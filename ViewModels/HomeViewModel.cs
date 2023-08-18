using ChatRooms.Models;

namespace ChatRooms.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
