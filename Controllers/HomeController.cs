using ChatRooms.Helpers;
using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace ChatRooms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=38eefb25a8168b";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Users = await _userRepository.GetAllUsers(); // add city to user model
                }
                else
                {
                    homeViewModel.Users = null;
                }
                return View(homeViewModel);
            }
            catch (Exception ex)
            {
                homeViewModel.Users = null;
            }

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}