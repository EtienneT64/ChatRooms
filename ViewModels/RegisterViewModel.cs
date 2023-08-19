using System.ComponentModel.DataAnnotations;

namespace ChatRooms.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username"), Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password"), Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", ErrorMessage = "Password must contain at least one number, uppercase, lowercase and symbol")]
        public string ConfirmPassword { get; set; }
    }
}
