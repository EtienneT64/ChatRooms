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
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(30), Display(Name = "Display Name")]
        public string DisplayName { get; set; } = string.Empty;

        [Required, StringLength(7)]
        public string DisplayNameColor { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }

        //public Status? Status { get; set; }

        public ICollection<Message> Messages { get; set; }
        public Chatroom Chatroom { get; set; }
    }
}
