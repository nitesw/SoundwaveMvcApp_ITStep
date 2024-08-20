using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Core.Dtos;
using SoundwaveMvcApp_ITStep.Models;
using System.Diagnostics;
using SoundwaveMvcApp_ITStep.Extensions;
using SoundwaveMvcApp_ITStep.Services;
using Core.Interfaces;
using System.Security.Claims;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            // TODO: optimize a isLiked
            var homePageData = homeService.GetHomePageData();
            ViewBag.LikedTracks = homePageData.LikedTracks;
            ViewBag.Playlists = homeService.GetUserPlaylists(UserId);

            return View(homePageData.Tracks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
