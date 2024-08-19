using Core.Interfaces;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundwaveMvcApp_ITStep.Services;
using System.Security.Claims;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly IPlaylistsService playlistService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public PlaylistsController(IPlaylistsService playlistService)
        {
            this.playlistService = playlistService;
        }

        public IActionResult Index()
        {
            return View(playlistService.GetPlaylists());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            ViewBag.UserId = UserId;
            return View("Upsert");
        }
        [HttpPost]
        public IActionResult Create(Playlist model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                ViewBag.UserId = UserId;
                return View("Upsert", model);
            }

            playlistService.CreateItem(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
