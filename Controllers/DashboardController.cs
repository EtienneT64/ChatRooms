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

            return View();
        }
    }
}
