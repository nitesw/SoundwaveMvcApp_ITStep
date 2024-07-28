using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class HomeService
    {
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;
        private SoundwaveDbContext ctx;

        public HomeService(IHttpContextAccessor contextAccessor, IMapper mapper, SoundwaveDbContext ctx)
        {
            this.httpContext = contextAccessor.HttpContext!;
            this.mapper = mapper;
            this.ctx = ctx;
        }

        public HomePageDataDto GetHomePageData()
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items") ?? new();
            var likedTracks = ctx.Tracks.Include(x => x.Genre).Where(x => ids.Contains(x.Id)).ToList();
            var mappedLikedTracks = mapper.Map<List<TrackDto>>(likedTracks);

            var tracks = ctx.Tracks
                .Where(x => !x.IsArchived)
                .Include(x => x.User)
                .ToList();
            var mappedTracks = mapper.Map<List<TrackDto>>(tracks);

            return new HomePageDataDto
            {
                Tracks = mappedTracks,
                LikedTracks = mappedLikedTracks
            };
        }
    }
}
