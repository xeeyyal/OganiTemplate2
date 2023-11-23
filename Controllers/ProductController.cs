using FrontToBack_2.DAL;
using FrontToBack_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack_2.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> dbProducts = await _context.Products.ToListAsync();
            if (dbProducts is null) return NotFound();
            return View(dbProducts);
        }
        public IActionResult Details()
        {
            List<Product> product = _context.Products.ToList();

            return View(product);
        }
    }
}
