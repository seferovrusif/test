﻿using Diana.Areas.Admin.ViewModels;
using Diana.ViewModels.ProductVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_project.Contexts;
using Pustok_project.Models;
namespace Pustok_project.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class ProductController : Controller
    {
            PustokDbContext _db { get; }

            public ProductController(PustokDbContext db)
            {
                _db = db;
            }

            public IActionResult Index()
            {

                return View(_db.Product.Select(p => new AdminProductListItemVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    Discount = p.Discount,
                    Category = p.Category,
                    ImageUrl = p.ImageUrl,
                    IsDeleted = p.IsDeleted,
                    Quantity = p.Quantity,
                    SellPrice = p.SellPrice
                }));
            }
            public IActionResult Create()
            {
                ViewBag.Categories = _db.Category;
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(ProductCreateVM vm)
            {
                if (vm.CostPrice > vm.SellPrice)
                {
                    ModelState.AddModelError("CostPrice", "Sell price must be bigger than cost price");
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.Categories = _db.Category;
                    return View(vm);
                }
                if (!await _db.Category.AnyAsync(c => c.Id == vm.CategoryId))
                {
                    ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Categories = _db.Category;
                    return View(vm);
                }
                Product prod = new Product
                {
                    Name = vm.Name,
                    About = vm.About,
                    Quantity = vm.Quantity,
                    Description = vm.Description,
                    Discount = vm.Discount,
                    ImageUrl = vm.ImageUrl,
                    CostPrice = vm.CostPrice,
                    SellPrice = vm.SellPrice,
                    CategoryId = vm.CategoryId
                };
                await _db.Product.AddAsync(prod);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }


