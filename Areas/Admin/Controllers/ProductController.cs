using FrontToBack_2.DAL;
using FrontToBack_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(p => p.Department).ToListAsync();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return View();

            bool result = _context.Products.Any(c => c.Name.ToLower().Trim() == product.Name.ToLower().Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu Kateqoriya artiq movcuddur.");
                return View();
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Product category = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (category is null) return NotFound();

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Product existed = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return NotFound();

            bool result = _context.Products.Any(c => c.Name == product.Name && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda product artiq movcuddur");
                return View();
            }
            existed.Name = product.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return NotFound();

            _context.Products.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();
            Product product = await _context.Products.Include(c => c.Department).FirstOrDefaultAsync(s => s.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
