using FrontToBack_2.DAL;
using FrontToBack_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }
    }
}
