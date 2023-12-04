using Microsoft.EntityFrameworkCore;
using Pustok_project.Models;

namespace Pustok_project.Contexts;
public class PustokDbContext:DbContext
{
    public PustokDbContext(DbContextOptions opt) : base(opt) { }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }


}
