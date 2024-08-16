using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class PlaylistsService
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
    }
}
