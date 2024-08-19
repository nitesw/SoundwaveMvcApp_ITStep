using Microsoft.AspNetCore.Mvc;
using Data.Data;
using AutoMapper;
using SoundwaveMvcApp_ITStep.Services;
using Microsoft.AspNetCore.Authorization;
using Core.Interfaces;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    [Authorize]
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
