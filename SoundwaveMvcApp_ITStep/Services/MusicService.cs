using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class MusicService : IMusicService
    {
        private readonly HttpContext httpContext;
        private SoundwaveDbContext ctx;
        private readonly IFilesService filesService;
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;

        public MusicService(IHttpContextAccessor contextAccessor, IMapper mapper, SoundwaveDbContext ctx, IFilesService filesService, IEmailSender emailSender)
        {
            this.httpContext = contextAccessor.HttpContext!;
            this.ctx = ctx;
            this.filesService = filesService;
            this.emailSender = emailSender;
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
        public async Task DeleteItem(int id)
        {
            var track = ctx.Tracks
                .Include(x => x.PlaylistTracks)
                .FirstOrDefault(p => p.Id == id);

            if (track == null) return;

            if (track.ImgUrl != null)
                await filesService.DeleteFile(track.ImgUrl);

            ctx.PlaylistTrack.RemoveRange(track.PlaylistTracks!);

            ctx.Tracks.Remove(track);
            ctx.SaveChanges();
        }

        public async Task CreateItem(TrackDto model, string userEmail)
        {
            var entity = mapper.Map<Track>(model);

            entity.ImgUrl = await filesService.SaveFile(model.Image, true);
            entity.TrackUrl = await filesService.SaveFile(model.Track, false);

            ctx.Tracks.Add(entity);
            ctx.SaveChanges();

            await emailSender.SendEmailAsync(userEmail, $"New Track: {entity.Title}", "<h1>You've created new track.</h1>");
        }

        public TrackDto EditItem(int id)
        {
            var track = ctx.Tracks.Find(id);
            if (track == null) return null!;

            return mapper.Map<TrackDto>(track);
        }
        public async Task EditItem(TrackDto model)
        {
            if (model.Image != null)
            {
                model.ImgUrl = await filesService.EditFile(model.ImgUrl, model.Image, true);
            }
            ctx.Tracks.Update(mapper.Map<Track>(model));
            ctx.SaveChanges();
        }

        public List<GenreDto> LoadGenres()
        {
            return mapper.Map<List<GenreDto>>(ctx.Genres);
        }

        public TrackDto GetDetails(int id)
        {
            var track = ctx.Tracks
                .Include(x => x.Genre)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
            if (track == null) return null!;

            return mapper.Map<TrackDto>(track);
        }

        public List<PlaylistDto> GetUserPlaylists(string userId)
        {
            var playlists = ctx.Playlists
               .Where(x => x.UserId == userId)
               .Include(x => x.User)
               .ToList();

            return mapper.Map<List<PlaylistDto>>(playlists);
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
