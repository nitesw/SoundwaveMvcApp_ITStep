using Microsoft.AspNetCore.Mvc;
using SoundwaveMvcApp_ITStep.Data;

namespace SoundwaveMvcApp_ITStep.Controllers
{
    public class UsersController : Controller
    {
        private SoundwaveDbContext ctx = new SoundwaveDbContext();

        public IActionResult Index()
        {
            var users = ctx.Users.ToList();

            return View(users);
        }
    }
}
