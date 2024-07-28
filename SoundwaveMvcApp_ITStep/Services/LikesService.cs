using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class LikesService
    {
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;
        private SoundwaveDbContext ctx;

        public LikesService(IHttpContextAccessor contextAccessor, IMapper mapper, SoundwaveDbContext ctx)
        {
            this.httpContext = contextAccessor.HttpContext!;
            this.mapper = mapper;
            this.ctx = ctx;
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items");
            if (ids == null) return 0;

            return ids.Count();
        }

        public List<TrackDto> GetLikes()
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items") ?? new();
            var tracks = ctx.Tracks.Include(x => x.Genre).Where(x => ids.Contains(x.Id)).ToList();

            return mapper.Map<List<TrackDto>>(tracks);
        }
        public bool AddItem(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items");
            if (ids == null) ids = new();
            if (ids.Contains(id)) return false;

            ids.Add(id);
            httpContext.Session.Set("liked_items", ids);
            return true;
        }
        public void RemoveItem(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items");
            if (ids == null || !ids.Contains(id)) return;

            ids.Remove(id);
            httpContext.Session.Set("liked_items", ids);
        }
    }
}
