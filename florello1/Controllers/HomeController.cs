using florello1.DAL;
using florello1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace florello1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                Products = await _context.Products.OrderByDescending(p => p.CreatedTime).Take(8)
                .Include(x => x.ProductImages.Where(pi => pi.IsPrimary != null))
                .ToListAsync(),
            };

            return View(homeVM);
        }
    }
}
