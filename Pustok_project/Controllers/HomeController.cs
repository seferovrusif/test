using Microsoft.AspNetCore.Mvc;
using Pustok_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Pustok_project.Contexts;

namespace Pustok_project.Controllers
{
    public class HomeController : Controller
    {
        PustokDbContext _context { get; }

        public HomeController(PustokDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }

    }
}