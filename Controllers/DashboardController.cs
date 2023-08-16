using ChatRooms.Interfaces;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepositiory, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepositiory;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            var userMessages = await _dashboardRepository.GetAllUserMessages();
            var dashboardViewModel = new DashboardViewModel()
            {
                Messages = userMessages
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currUserId);
            if (user == null) return View("Error");
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currUserId,
                DisplayNameColor = user.DisplayNameColor,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(editUserViewModel);
        }
    }
}
