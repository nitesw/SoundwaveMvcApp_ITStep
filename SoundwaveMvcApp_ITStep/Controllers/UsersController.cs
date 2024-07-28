using Microsoft.AspNetCore.Mvc;
using Data.Data;
using AutoMapper;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private SoundwaveDbContext ctx;

        public UsersController(IMapper mapper, SoundwaveDbContext ctx)
        {
            this.mapper = mapper;
            this.ctx = ctx;
        }

        public IActionResult Index()
        {
            var users = ctx.Users.ToList();

            return View(users);
        }
    }
}
