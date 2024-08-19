using Data.Data;
using Core.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class UsersService
    {
        private SoundwaveDbContext ctx;
        private readonly IMapper mapper;

        public UsersService(IMapper mapper, SoundwaveDbContext ctx)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }

        public List<UserDto> GetUsers()
        {
            var users = ctx.Users
                .Include(x => x.Tracks)
                .Include(x => x.Playlists)
                .ToList();

            return mapper.Map<List<UserDto>>(users);
        }
    }
}
