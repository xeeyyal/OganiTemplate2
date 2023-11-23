using FrontToBack_2.DAL;
using FrontToBack_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid) return View();

            bool result = _context.Blogs.Any(c => c.Title.Trim() == blog.Title.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu Blog artiq movcuddur.");
                return View();
            }
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (blog is null) return NotFound();

            return View(blog);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Blog blog)
        {
            if (!ModelState.IsValid) return View();

            Blog existed = await _context.Blogs.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return NotFound();

            bool result = _context.Blogs.Any(c => c.Title == blog.Title && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda product artiq movcuddur");
                return View();
            }
            existed.Title = blog.Title;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Blog existed = await _context.Blogs.FirstOrDefaultAsync(c => c.Id == id);

            if (existed is null) return NotFound();

            _context.Blogs.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return NotFound();

            return View(blog);
        }
    }
}
