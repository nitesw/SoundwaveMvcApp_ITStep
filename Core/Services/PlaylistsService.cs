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
        // TODO: DeleteItem, EditItem

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
            // TODO: add logic
            Console.WriteLine("\n\nRemoved.\n\n");
        }
    }
}
