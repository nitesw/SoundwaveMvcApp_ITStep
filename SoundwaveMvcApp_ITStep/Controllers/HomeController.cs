using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Data;
using SoundwaveMvcApp_ITStep.Models;
using System.Diagnostics;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class HomeController : Controller
    {
        private SoundwaveDbContext ctx = new SoundwaveDbContext();

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            var tracks = ctx.Tracks
                .Where(x => !x.IsArchived)
                .Include(x => x.User)
                .ToList();

            return View(tracks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
