using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Core.Dtos;
using Data.Entities;
using System.Diagnostics;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class MusicController : Controller
    {
        private SoundwaveDbContext ctx = new SoundwaveDbContext();
        private readonly IMapper mapper;

        public MusicController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var music = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => !x.IsArchived)
                .ToList();

            return View(mapper.Map<List<TrackDto>>(music));
        }

        public IActionResult Archive()
        {
            var music = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => x.IsArchived)
                .ToList();

            return View(mapper.Map<List<TrackDto>>(music));
        }
        public IActionResult ArchiveTrack(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();
            track.IsArchived = true;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult RestoreTrack(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();
            track.IsArchived = false;
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }
        public IActionResult DeleteTrack(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();
            ctx.Tracks.Remove(track);
            ctx.SaveChanges();

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

            ctx.Tracks.Add(mapper.Map<Track>(model));
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            LoadGenres();
            ViewBag.UploadMode = false;
            return View("Upsert", mapper.Map<TrackDto>(track));
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

            ctx.Tracks.Update(mapper.Map<Track>(model));
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        private void LoadGenres()
        {
            ViewBag.Genres = new SelectList(mapper.Map<List<GenreDto>>(ctx.Genres), "Id", "Name");
        }
    }
}
