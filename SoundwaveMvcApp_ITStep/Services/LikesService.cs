using AutoMapper;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using SoundwaveMvcApp_ITStep.Extensions;

namespace SoundwaveMvcApp_ITStep.Services
{
    public class LikesService
    {
        private readonly HttpContext httpContext;

        public LikesService(IHttpContextAccessor contextAccessor)
        {
            this.httpContext = contextAccessor.HttpContext!;
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>("liked_items");
            if (ids == null) return 0;

            return ids.Count();
        }
    }
}
