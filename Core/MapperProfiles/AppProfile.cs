using AutoMapper;
using Core.Dtos;
using Data.Entities;

namespace Core.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<TrackDto, Track>().ReverseMap();
            CreateMap<GenreDto, Genre>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(x => x.TrackCount, opt => opt.MapFrom(src => src.Tracks.Count))
                .ForMember(x => x.PlaylistCount, opt => opt.MapFrom(src => src.Playlists.Count))
                .ReverseMap();
            CreateMap<Playlist, PlaylistDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ReverseMap();
        }
    }
}
