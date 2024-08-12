using AutoMapper;
using Core.Dtos;
using Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundwaveMvcApp_ITStep.Extensions;
using SoundwaveMvcApp_ITStep.Services;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class LikesController : Controller
    {
        private readonly LikesService likesService;

        public LikesController(LikesService likesService)
        {
            this.likesService = likesService;
        }

        public IActionResult Index()
        {
            return View(likesService.GetLikes());
        }
        public IActionResult AddLike(int id, string? returnUrl)
        {
            likesService.AddItem(id);

            return Redirect(returnUrl ?? "/");
        }
        public IActionResult RemoveLike(int id, string? returnUrl)
        {
            likesService.RemoveItem(id);

            return Redirect(returnUrl ?? "/");
        }
    }
}
