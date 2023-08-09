using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "Email Address")]
		[Required(ErrorMessage ="Please enter email address")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
