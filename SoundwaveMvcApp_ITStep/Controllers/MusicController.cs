using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Data;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class MusicController : Controller
    {
        private SoundwaveDbContext ctx = new SoundwaveDbContext();

        public IActionResult Index()
        {
            var music = ctx.Songs
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => !x.IsArchived)
                .ToList();

            return View(music);
        }

        public IActionResult Archive()
        {
            var music = ctx.Songs
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => x.IsArchived)
                .ToList();

            return View(music);
        }
        public IActionResult ArchiveSong(int id)
        {
            var song = ctx.Songs.Find(id);
            if (song == null) return NotFound();
            song.IsArchived = true;
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }
        
        public IActionResult RestoreSong(int id)
        {
            var song = ctx.Songs.Find(id);
            if (song == null) return NotFound();
            song.IsArchived = false;
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }
        public IActionResult DeleteSong(int id)
        {
            var song = ctx.Songs.Find(id);
            if (song == null) return NotFound();
            ctx.Songs.Remove(song);
            ctx.SaveChanges();

            return RedirectToAction("Archive");
        }
    }
}
