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
            var userOwnedChatrooms = await _dashboardRepository.GetAllUserOwnedChatrooms();
            var userPinnedChatrooms = await _dashboardRepository.GetAllUserPinnedChatrooms();
            var dashboardViewModel = new DashboardViewModel()
            {
                OwnedChatrooms = userOwnedChatrooms,
                PinnedChatrooms = userPinnedChatrooms,
            };

            return View(dashboardViewModel);

        }
    }
}
