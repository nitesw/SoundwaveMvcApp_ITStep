using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMusicService
    {
        List<TrackDto> GetTracks();
        List<TrackDto> GetArchivedTracks();
        void ArchiveItem(int id);
        void RestoreItem(int id);
        void DeleteItem(int id);
        void CreateItem(TrackDto model);
        TrackDto EditItem(int id);
        void EditItem(TrackDto model);
        List<GenreDto> LoadGenres();
        TrackDto GetDetails(int id);
        bool IsLiked(int id);
    }
}
