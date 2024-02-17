using FLL.Data;
using FLL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FLL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (Request.Cookies.ContainsKey("userId"))
            {
                var exhibitId = _context.Rating.FirstOrDefault(
                    e => e.UserId.ToString() == Request.Cookies.First(kvp => kvp.Key == "userId").Value)?.ExhibitId;

                if (exhibitId != null)
                {
                    return View(new HomePageModel()
                    {
                        Exhibits = await _context.Exhibit.Include(e => e.Ratings).OrderBy(e => e.ExhibitName).ToListAsync(),
                        LikedExhibit = await _context.Exhibit.FirstAsync(e => e.ExhibitId == exhibitId)
                    }); ;
                }
            }

            return View(new HomePageModel()
            {
                Exhibits = await _context.Exhibit.Include(e => e.Ratings).OrderBy(e => e.ExhibitName).ToListAsync(),
            });
        }

        public IActionResult AuthenticationCallback()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
