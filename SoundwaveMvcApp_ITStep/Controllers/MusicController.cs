using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Data;
using SoundwaveMvcApp_ITStep.Entities;
using System.Diagnostics;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class MusicController : Controller
    {
        private SoundwaveDbContext ctx = new SoundwaveDbContext();

        public IActionResult Index()
        {
            var music = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => !x.IsArchived)
                .ToList();

            return View(music);
        }

        public IActionResult Archive()
        {
            var music = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => x.IsArchived)
                .ToList();

            return View(music);
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

            return View();
        }
        [HttpPost]
        public IActionResult Create(Track model)
        {
            ctx.Tracks.Add(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return NotFound();

            LoadGenres();
            ViewBag.CreateMode = false;
            return View(track);
        }
        [HttpPost]
        public IActionResult Edit(Track model)
        {
            ctx.Tracks.Update(model);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        private void LoadGenres()
        {
            ViewBag.Genres = new SelectList(ctx.Genres.ToList(), "Id", "Name");
        }
    }
}
