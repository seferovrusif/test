using Microsoft.AspNetCore.Mvc;
using Pustok_project.ViewModels.Category;
using Pustok_project.Contexts;
using Microsoft.EntityFrameworkCore;
using Pustok_project.ViewModels.SliderVM;

namespace Pustok_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDbContext _db { get; }
        public CategoryController(PustokDbContext db)
        {
            _db = db;
        }



        public async Task<IActionResult> Index()
        {
            return View( await _db.Category.Select(s => new CategoryListItemVM
            {
                Id = s.Id,
                Name = s.Name,
            }).ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (await _db.Category.AnyAsync(x => x.Name == vm.Name))
            {
                ModelState.AddModelError("Name", vm.Name + " already exist");
                return View(vm);
            }
            await _db.Category.AddAsync(new Models.Category { Name = vm.Name });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
