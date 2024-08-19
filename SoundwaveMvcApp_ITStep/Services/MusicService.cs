using AutoMapper;
using Core.Dtos;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class MusicService
    {
        private readonly HttpContext httpContext;
        private SoundwaveDbContext ctx;
        private readonly IMapper mapper;

        public MusicService(IHttpContextAccessor contextAccessor, IMapper mapper, SoundwaveDbContext ctx)
        {
            this.httpContext = contextAccessor.HttpContext!;
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public List<TrackDto> GetTracks()
        {
            var music = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => !x.IsArchived)
                .ToList();

            return mapper.Map<List<TrackDto>>(music);
        }

        public List<TrackDto> GetArchivedTracks()
        {
            var music = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .Where(x => x.IsArchived)
                .ToList();

            return mapper.Map<List<TrackDto>>(music);
        }
        public void ArchiveItem(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return;
            ctx.Entry(track).State = EntityState.Modified;
            track.IsArchived = true;
            ctx.SaveChanges();
        }

        public void RestoreItem(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return;
            ctx.Entry(track).State = EntityState.Modified;
            track.IsArchived = false;
            ctx.SaveChanges();
        }
        public void DeleteItem(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return;
            ctx.Tracks.Remove(track);
            ctx.SaveChanges();
        }

        public void CreateItem(TrackDto model)
        {
            ctx.Tracks.Add(mapper.Map<Track>(model));
            ctx.SaveChanges();
        }

        public TrackDto EditItem(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return null!;

            return mapper.Map<TrackDto>(track);
        }
        public void EditItem(TrackDto model)
        {
            ctx.Tracks.Update(mapper.Map<Track>(model));
            ctx.SaveChanges();
        }

        public List<GenreDto> LoadGenres()
        {
            return mapper.Map<List<GenreDto>>(ctx.Genres);
        }

        public TrackDto GetDetails(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return null!;

            return mapper.Map<TrackDto>(track);
        }

        public bool IsLiked(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items");
            if (ids != null)
            {
                var track = ctx.Tracks.Find(id);
                if (ids.Contains(track.Id))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
