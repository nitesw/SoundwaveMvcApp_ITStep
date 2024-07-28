using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class LikesController : Controller
    {
        private readonly IMapper mapper;
        private SoundwaveDbContext ctx;

        public LikesController(IMapper mapper, SoundwaveDbContext ctx)
        {
            this.mapper = mapper;
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items") ?? new();
            var tracks = ctx.Tracks.Include(x => x.Genre).Where(x => ids.Contains(x.Id)).ToList();

            return View(mapper.Map<List<TrackDto>>(tracks));
        }
        public IActionResult AddLike(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items");
            if (ids == null) ids = new();
            if (ids.Contains(id)) return RedirectToAction("RemoveLike", new { id });

            ids.Add(id);
            HttpContext.Session.Set("liked_items", ids);

            return RedirectToAction("Index");
        }
        public IActionResult RemoveLike(int id)
        {
            var ids = HttpContext.Session.Get<List<int>>("liked_items");
            if (ids == null || !ids.Contains(id)) return NotFound();

            ids.Remove(id);
            HttpContext.Session.Set("liked_items", ids);

            return RedirectToAction("Index");
        }
    }
}
