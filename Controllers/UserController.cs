using ChatRooms.Interfaces;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
            };
            return View(userDetailViewModel);
        }
    }
}
