using ChatRooms.Interfaces;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepositiory;

        public DashboardController(IDashboardRepository dashboardRepositiory)
        {
            _dashboardRepositiory = dashboardRepositiory;
        }
        public async Task<IActionResult> Index()
        {
            var userMessages = await _dashboardRepositiory.GetAllUserMessages();
            var dashboardViewModel = new DashboardViewModel()
            {
                Messages = userMessages
            };
            return View(dashboardViewModel);
        }
    }
}
