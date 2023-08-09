using System.ComponentModel.DataAnnotations;

namespace ChatRooms.Models
{

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30), MinLength(2)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, StringLength(30), Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required, StringLength(7)]
        public string DisplayNameColor { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; }

        public int? ChatroomId { get; set; }

        public ICollection<Message>? Messages { get; set; }
        public ICollection<Chatroom>? Chatrooms { get; set; }
    }
}
