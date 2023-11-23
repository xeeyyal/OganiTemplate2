using FrontToBack_2.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack_2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt):base(opt)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

