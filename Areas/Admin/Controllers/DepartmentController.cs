using FrontToBack_2.DAL;
using FrontToBack_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace FrontToBack_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Departament> departament=_context.Departaments.ToList();
            return View(departament);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create( Departament departament)
        {
            if (!ModelState.IsValid) return View();

            bool result = _context.Departaments.Any(d => d.Name.Trim() == departament.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "Bu Department artiq movcuddur.");
                return View();
            }
            await _context.Departaments.AddAsync(departament);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Departament departament = await _context.Departaments.FirstOrDefaultAsync(d => d.Id == id);

            if (departament is null) return NotFound();

            return View(departament);

        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Departament departament)
        {
            if (!ModelState.IsValid) return View();

            Departament existed = await _context.Departaments.FirstOrDefaultAsync(d => d.Id == id);

            if (existed is null) return NotFound();

            bool result = _context.Departaments.Any(c => c.Name == departament.Name && c.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda department artiq movcuddur");
                return View();
            }
            existed.Name = departament.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Departament existed = await _context.Departaments.FirstOrDefaultAsync(d => d.Id == id);

            if (existed is null) return NotFound();

            _context.Departaments.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            Departament departament = await _context.Departaments.FirstOrDefaultAsync(d => d.Id == id);
            if (departament == null) return NotFound();

            return View(departament);
        }
    }
}
