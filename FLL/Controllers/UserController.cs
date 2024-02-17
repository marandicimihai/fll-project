using FLL.Data;
using FLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FLL.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CreateUser()
        {
            var newUser = new User()
            {
                UserId = Guid.NewGuid()
            };

            await _context.User.AddAsync (newUser);

            await _context.SaveChangesAsync();

            return View(newUser.UserId);
        }

        [Route("user/authenticate/{userGuid}")]
        public IActionResult Authenticate(string userGuid)
        {
            if (!Guid.TryParse(userGuid, out var userId))
                return BadRequest();

            if (Request.Cookies.ContainsKey("userId"))
                return View("AlreadyAuthenticated");

            Response.Cookies.Append("userId", userId.ToString(), new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(4)
            });

            return RedirectToAction("AuthenticationCallback", "Home");
        }

        public IActionResult Logout()
        {
            if (Request.Cookies.ContainsKey("userId"))
                Response.Cookies.Delete("userId");

            return RedirectToAction("AuthenticationCallback", "Home");
        }
    }
}
