using Microsoft.AspNetCore.Mvc;
using Data.Data;
using AutoMapper;
using SoundwaveMvcApp_ITStep.Services;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class UsersController : Controller
    {
        private UsersService usersService;

        public UsersController(UsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return View(usersService.GetUsers());
        }
    }
}
