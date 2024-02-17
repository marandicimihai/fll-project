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
        [Route("user/add/{userGuid}")]
        public async Task<IActionResult> AddUser(string userGuid)
        {
            if (!Guid.TryParse(userGuid, out var userId))
                return BadRequest();

            var newUser = new User()
            {
                UserId = userId
            };

            await _context.User.AddAsync(newUser);

            await _context.SaveChangesAsync();

            return View("CreateUser", userId);
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
