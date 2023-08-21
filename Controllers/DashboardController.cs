using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using CloudinaryDotNet.Actions;
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
        private void MapUserEdit(User user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.ProfileImageUrl = photoResult.Url.ToString();
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
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
