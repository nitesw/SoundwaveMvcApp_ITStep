using AutoMapper;
using Core.Dtos;
using Core.Interfaces;
using Data.Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class PlaylistsService : IPlaylistsService
    {
        private SoundwaveDbContext ctx;
        private readonly IMapper mapper;

        public PlaylistsService(IMapper mapper, SoundwaveDbContext ctx)
        {
            this.ctx = ctx;
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

        public void CreateItem(Playlist model)
        {
            ctx.Playlists.Add(model);
            ctx.SaveChanges();
        }
        public void DeleteItem(int id)
        {
            var playlist = ctx.Playlists
                .Include(p => p.PlaylistTracks)
                .FirstOrDefault(p => p.Id == id);

            if (playlist == null) return;

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
    }
}
