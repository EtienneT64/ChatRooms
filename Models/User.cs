using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{
    //public enum Status
    //{
    //    Guest, Member, Admin
    //}
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string DisplayNameColor { get; set; }
        public DateTime RegisterDate { get; set; }

        //public Status? Status { get; set; }

        public ICollection<Message> Messages { get; set; }

        // testing git
    }
}
