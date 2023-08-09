using ChatRooms.Data;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChatRooms.Controllers
{


	public class AccController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ChatroomContext _context;

		public AccController(UserManager<User> userManager, SignInManager<User> signInManager, ChatroomContext context)
		{
			_context = context;
			_signInManager = signInManager;
			_userManager = userManager;
		}
		public IActionResult Login()
		{
			var response = new LoginVM();
			return View(response);
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if (!ModelState.IsValid) return View();
			var user = await _userManager.FindByEmailAsync(loginVM.Email);

			if (user != null)
			{
				//User is found,  Check password
				var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
				if (passwordCheck)
				{
					//password correct, sign in
					var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
				}

				//Password is incorrect
				TempData["Error"] = "Wrong details entered";
				return View(loginVM);
			}
			//User not found
			TempData["Error"] = "Wrong details";
			return View(loginVM);
		}
	}

}
