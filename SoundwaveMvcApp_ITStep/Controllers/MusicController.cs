using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Core.Dtos;
using Data.Entities;
using System.Diagnostics;
using SoundwaveMvcApp_ITStep.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Core.Interfaces;
using SoundwaveMvcApp_ITStep.SeedExtensions;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class MusicController : Controller
    {
        private readonly IMusicService musicService;
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private string UserEmail => User.FindFirstValue(ClaimTypes.Email)!;

        public MusicController(IMusicService musicService)
        {
            this.musicService = musicService;
        }

        public IActionResult Index()
        {
            return View(musicService.GetTracks());
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var isLiked = musicService.IsLiked(id);
            ViewBag.IsLiked = isLiked;

            return View(musicService.GetDetails(id));
        }

        public IActionResult Archive()
        {
            return View(musicService.GetArchivedTracks());
        }
        public IActionResult ArchiveTrack(int id)
        {
            musicService.ArchiveItem(id);

            return RedirectToAction("Index");
        }

        public IActionResult RestoreTrack(int id)
        {
            musicService.RestoreItem(id);

            return RedirectToAction("Archive");
        }
        public async Task<IActionResult> DeleteTrack(int id)
        {
            await musicService.DeleteItem(id);

            return RedirectToAction("Archive");
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadGenres();
            ViewBag.UploadMode = true;
            ViewBag.UserId = UserId;
            return View("Upsert");
        }
        [HttpPost]
        public async Task<IActionResult> Create(TrackDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UploadMode = true;
                ViewBag.UserId = UserId;
                LoadGenres();
                return View("Upsert", model);
            }

            await musicService.CreateItem(model, UserEmail);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            LoadGenres();
            ViewBag.UploadMode = false;
            return View("Upsert", musicService.EditItem(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TrackDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UploadMode = false;
                LoadGenres();
                return View("Upsert", model);
            }

            await musicService.EditItem(model);

            return RedirectToAction("Index");
        }

        private void LoadGenres()
        {
            ViewBag.Genres = new SelectList(musicService.LoadGenres(), "Id", "Name");
        }
    }
}
