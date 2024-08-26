using Microsoft.AspNetCore.Mvc;
using Data.Data;
using AutoMapper;
using SoundwaveMvcApp_ITStep.Services;
using Microsoft.AspNetCore.Authorization;
using Core.Interfaces;
using SoundwaveMvcApp_ITStep.SeedExtensions;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class UsersController : Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return View(usersService.GetUsers());
        }
    }
}
