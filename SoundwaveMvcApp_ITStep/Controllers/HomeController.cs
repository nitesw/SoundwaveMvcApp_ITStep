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

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            // TODO: optimize a isLiked
            var homePageData = homeService.GetHomePageData();
            ViewBag.LikedTracks = homePageData.LikedTracks;

            return View(homePageData.Tracks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
