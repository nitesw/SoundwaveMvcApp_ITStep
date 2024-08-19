using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILikesService
    {
        int GetCount();
        List<TrackDto> GetLikes();
        void AddItem(int id);
        void RemoveItem(int id);
    }
}
