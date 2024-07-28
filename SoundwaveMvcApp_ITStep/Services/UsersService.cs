using Data.Data;
using Core.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

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
            var users = ctx.Users.ToList();

            return mapper.Map<List<UserDto>>(users);
        }
    }
}
