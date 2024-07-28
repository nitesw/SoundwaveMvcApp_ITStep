using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Core.Dtos;
using SoundwaveMvcApp_ITStep.Models;
using System.Diagnostics;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private SoundwaveDbContext ctx;

        public HomeController(IMapper mapper, SoundwaveDbContext ctx)
        {
            this.mapper = mapper;
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items") ?? new();
            var likedTracks = ctx.Tracks.Include(x => x.Genre).Where(x => ids.Contains(x.Id)).ToList();
            ViewBag.LikedTracks = mapper.Map<List<TrackDto>>(likedTracks);

            var tracks = ctx.Tracks
                .Where(x => !x.IsArchived)
                .Include(x => x.User)
                .ToList();

            return View(mapper.Map<List<TrackDto>>(tracks));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
