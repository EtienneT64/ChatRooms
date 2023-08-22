using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;

        private readonly UserManager<User> _userManager;

        public UserController(IUserRepository userRepository, IPhotoService photoService, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _photoService = photoService;

            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var currUser = HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(currUser);

            var userVM = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
            };

            return View(userVM);
        }

        // GET: User/Edit/1
        public async Task<IActionResult> Edit(string? id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return View("Error");
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVM);
        }

        // POST: User/Edit/1
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string? id, UserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("Edit", userVM);
            }

            //var user = await _userRepository.GetUserByIdAsyncNoTracking(id);
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return View("Error");

            _context.Entry(user).Reload();

            var photoResult = await _photoService.AddProfilePictureAsync(userVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(userVM);
            }

            if (!string.IsNullOrEmpty(user.ProfileImageUrl))
            {
                _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
            }

            //var editedUser = new User
            //{
            //    Id = id,
            //    UserName = userVM.UserName,
            //    ProfileImageUrl = photoResult.Url.ToString(),
            //};

            user.UserName = userVM.UserName;
            user.ProfileImageUrl = photoResult.Url.ToString();

            await _userManager.UpdateAsync(user);
            //_userRepository.Update(editedUser);

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(string id)
        //{

        //}
    }
}
