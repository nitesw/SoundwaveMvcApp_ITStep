using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundwaveMvcApp_ITStep.Services;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    [Authorize]
    public class PlaylistsController : Controller
    {
        private readonly PlaylistsService playlistService;

        public PlaylistsController(PlaylistsService playlistService)
        {
            this.playlistService = playlistService;
        }

        public IActionResult Index()
        {
            return View(playlistService.GetPlaylists());
        }
        
        public IActionResult Create()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
