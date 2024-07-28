using AutoMapper;
using Core.Dtos;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class MusicService
    {
        private SoundwaveDbContext ctx;
        private readonly IMapper mapper;

        public MusicService(IMapper mapper, SoundwaveDbContext ctx)
        {
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
            track.IsArchived = true;
            ctx.SaveChanges();
        }

        public void RestoreItem(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return;
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
    }
}
