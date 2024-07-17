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
        }
    }
}
