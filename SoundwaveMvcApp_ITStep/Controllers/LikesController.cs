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
        public IActionResult AddLike(int id)
        {
            var result = likesService.AddItem(id);
            if (!result)
            {
                return RedirectToAction("RemoveLike", new { id });
            }

            return RedirectToAction("Index", "Home");
        }
        public IActionResult RemoveLike(int id)
        {
            likesService.RemoveItem(id);

            return RedirectToAction("Index");
        }
    }
}
