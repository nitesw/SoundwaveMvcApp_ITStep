using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class PlaylistsService : IPlaylistsService
    {
        private readonly HttpContext httpContext;
        private SoundwaveDbContext ctx;
        private readonly IEmailSender emailSender;
        private readonly IFilesService filesService;
        private readonly IMapper mapper;

        public PlaylistsService(IMapper mapper, SoundwaveDbContext ctx, IEmailSender emailSender, IFilesService filesService, IHttpContextAccessor contextAccessor)
        {
            this.httpContext = contextAccessor.HttpContext!;
            this.ctx = ctx;
            this.emailSender = emailSender;
            this.filesService = filesService;
            this.mapper = mapper;
        }

        public List<PlaylistDto> GetPlaylists(string userId)
        {
            var playlists = ctx.Playlists
                .Include(x => x.User)
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Track)
                .Where(x => x.UserId == userId)
                .ToList();

            return mapper.Map<List<PlaylistDto>>(playlists);
        }
        public PlaylistDto GetPlaylist(int playlistId, string userId)
        {
            var playlist = ctx.Playlists
                .Include(x => x.User)
                .Include(x => x.PlaylistTracks!)
                .ThenInclude(x => x.Track)
                .ThenInclude(x => x.User)
                .Where(x => x.UserId == userId)
                .FirstOrDefault(x => x.Id == playlistId);

            if (playlist == null) return null;

            return mapper.Map<PlaylistDto>(playlist);
        }

        public async Task CreateItem(PlaylistDto model, string userEmail)
        {
            var user = await ctx.Users
                .AsTracking()
                .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var entity = mapper.Map<Playlist>(model);

            entity.ImgUrl = await filesService.SaveFile(model.Image, true);
            entity.UserId = user.Id;
            ctx.Entry(user).State = EntityState.Unchanged;
            entity.User = user;

            ctx.Playlists.Add(entity);

            await ctx.SaveChangesAsync();

            await emailSender.SendEmailAsync(userEmail, $"New Playlist: {model.Title}", $"<h1>You've created new playlist on Soundwave</h1>");
        }

        public async Task DeleteItem(int id)
        {
            var playlist = ctx.Playlists
                .Include(p => p.PlaylistTracks)
                .FirstOrDefault(p => p.Id == id);

            if (playlist == null) return;

            if (playlist.ImgUrl != null)
                await filesService.DeleteFile(playlist.ImgUrl);

            ctx.PlaylistTrack.RemoveRange(playlist.PlaylistTracks!);

            ctx.Playlists.Remove(playlist);
            ctx.SaveChanges();
        }

        public void AddTrackToPlaylist(int playlistId, int trackId)
        {
            bool exists = ctx.PlaylistTrack.Any(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);

            if(!exists)
            {
                PlaylistTrack playlistTrack = new PlaylistTrack
                {
                    PlaylistId = playlistId,
                    TrackId = trackId
                };

                ctx.PlaylistTrack.Add(playlistTrack);
                ctx.SaveChanges();
            }
            else
            {
                RemoveTrackFromPlaylist(playlistId, trackId);
            }
        }
        public void RemoveTrackFromPlaylist(int playlistId, int trackId)
        {
            bool exists = ctx.PlaylistTrack.Any(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
            if (exists)
            {
                PlaylistTrack toDelete = ctx.PlaylistTrack.FirstOrDefault(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId)!;

                ctx.PlaylistTrack.Remove(toDelete);
                ctx.SaveChanges();
            }
            else
            {
                AddTrackToPlaylist(playlistId, trackId);
            }
        }

        public PlaylistDto EditItem(int id)
        {
            var playlist = ctx.Playlists.Find(id);
            if (playlist == null) return null!;

            return mapper.Map<PlaylistDto>(playlist);
        }
        public async Task EditItem(PlaylistDto model)
        {
            var playlist = ctx.Playlists.FirstOrDefault(x => x.Id == model.Id);
            if (playlist == null) return;

            if (model.Image != null)
            {
                model.ImgUrl = await filesService.EditFile(model.ImgUrl, model.Image, true);
            }

            mapper.Map(model, playlist);
            ctx.Entry(playlist).State = EntityState.Modified;

            ctx.SaveChanges();
        }

        public List<TrackDto> LikedTracks()
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items") ?? new();
            var likedTracks = ctx.Tracks.Include(x => x.Genre).Include(x => x.User).Where(x => ids.Contains(x.Id)).ToList();

            return mapper.Map<List<TrackDto>>(likedTracks);
        }
    }
}
