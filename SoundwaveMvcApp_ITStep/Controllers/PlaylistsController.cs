using Core.Dtos;
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
        private string UserEmail => User.FindFirstValue(ClaimTypes.Email)!;

        public PlaylistsController(IPlaylistsService playlistService)
        {
            this.playlistService = playlistService;
        }

        public IActionResult Index()
        {
            return View(playlistService.GetPlaylists(UserId));
        }
        public IActionResult ShowPlaylist(int playlistId)
        {
            var playlist = playlistService.GetPlaylist(playlistId, UserId);

            if (playlist != null)
            {
                return View(playlist);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            ViewBag.UserId = UserId;
            return View("Upsert");
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlaylistDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = true;
                ViewBag.UserId = UserId;
                return View("Upsert", model);
            }

            await playlistService.CreateItem(model, UserEmail);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id, string? returnUrl)
        {
            playlistService.DeleteItem(id);

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("Upsert", playlistService.EditItem(id));
        }
        [HttpPost]
        public IActionResult Edit(PlaylistDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CreateMode = false;
                ViewBag.UserId = UserId;
                return View("Upsert", model);
            }

            playlistService.EditItem(model);
            return RedirectToAction("Index");
        }

        public IActionResult AddTrack(int playlistId, int trackId, string? returnUrl)
        {
            playlistService.AddTrackToPlaylist(playlistId, trackId);

            return Redirect(returnUrl ?? "/");
        }
        public IActionResult RemoveTrack(int playlistId, int trackId, string? returnUrl)
        {
            playlistService.AddTrackToPlaylist(playlistId, trackId);

            return Redirect(returnUrl ?? "/");
        }
    }
}
