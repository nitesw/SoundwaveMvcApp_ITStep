using Core.Dtos;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPlaylistsService
    {
        List<PlaylistDto> GetPlaylists(string userId);
        PlaylistDto GetPlaylist(int playlistId, string userId);
        Task CreateItem(PlaylistDto model, string userEmail);
        Task DeleteItem(int id);
        PlaylistDto EditItem(int id);
        Task EditItem(PlaylistDto model);
        void AddTrackToPlaylist(int playlistId, int trackId);
        void RemoveTrackFromPlaylist(int playlistId, int trackId);
    }
}
