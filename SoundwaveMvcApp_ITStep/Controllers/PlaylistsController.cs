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
        private readonly PlaylistsService playlistService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public PlaylistsController(PlaylistsService playlistService)
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

            playlistService.CreateItem(model, UserId);

            return RedirectToAction(nameof(Index));
        }
    }
}
