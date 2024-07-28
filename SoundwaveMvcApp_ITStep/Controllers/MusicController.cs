using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Core.Dtos;
using Data.Entities;
using System.Diagnostics;
using SoundwaveMvcApp_ITStep.Services;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class MusicController : Controller
    {
        private readonly MusicService musicService;

        public MusicController(MusicService musicService)
        {
            this.musicService = musicService;
        }

        public IActionResult Index()
        {
            return View(musicService.GetTracks());
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
        public IActionResult DeleteTrack(int id)
        {
            musicService.DeleteItem(id);

            return RedirectToAction("Archive");
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadGenres();
            ViewBag.UploadMode = true;
            return View("Upsert");
        }
        [HttpPost]
        public IActionResult Create(TrackDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UploadMode = true;
                LoadGenres();
                return View("Upsert", model);
            }

            musicService.CreateItem(model);

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
        public IActionResult Edit(TrackDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UploadMode = false;
                LoadGenres();
                return View("Upsert", model);
            }

            musicService.EditItem(model);

            return RedirectToAction("Index");
        }

        private void LoadGenres()
        {
            ViewBag.Genres = new SelectList(musicService.LoadGenres(), "Id", "Name");
        }
    }
}
