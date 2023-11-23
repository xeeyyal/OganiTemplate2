using FrontToBack_2.DAL;
using FrontToBack_2.Models;
using FrontToBack_2.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack_2.Controllers
{
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
            List<Departament> departaments = _context.Departaments.ToList();
            List<Blog> blogs = _context.Blogs.ToList();

            HomeVM vm = new()
            {
                Products=products,
                Departaments=departaments,
                Blogs=blogs
            };

            return View(vm);
        }
    }
}
