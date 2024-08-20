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
        void CreateItem(Playlist model);
        void DeleteItem(int id);
        void AddTrackToPlaylist(int playlistId, int trackId);
        void RemoveTrackFromPlaylist(int playlistId, int trackId);
    }
}
