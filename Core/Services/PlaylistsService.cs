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

        public List<PlaylistDto> GetPlaylists()
        {
            var playlists = ctx.Playlists
                .Include(x => x.User)
                .ToList();

            return mapper.Map<List<PlaylistDto>>(playlists);
        }

        public void CreateItem(Playlist model)
        {
            ctx.Playlists.Add(model);
            ctx.SaveChanges();
        }
    }
}
