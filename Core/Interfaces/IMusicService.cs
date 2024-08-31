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
        Task DeleteItem(int id);
        Task CreateItem(TrackDto model, string userEmail);
        TrackDto EditItem(int id);
        Task EditItem(TrackDto model);
        List<GenreDto> LoadGenres();
        TrackDto GetDetails(int id);
        bool IsLiked(int id);
        List<PlaylistDto> GetUserPlaylists(string userId);
    }
}
