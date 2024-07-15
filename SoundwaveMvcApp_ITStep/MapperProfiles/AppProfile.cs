using AutoMapper;
using SoundwaveMvcApp_ITStep.Dtos;
using SoundwaveMvcApp_ITStep.Entities;

namespace SoundwaveMvcApp_ITStep.MapperProfiles
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<TrackDto, Track>().ReverseMap();
        }
    }
}
