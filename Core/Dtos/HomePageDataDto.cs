using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class HomePageDataDto
    {
        public List<TrackDto>? Tracks { get; set; }
        public List<TrackDto>? LikedTracks { get; set; }
    }
}
