using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;
using OnlineStore.DB;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private readonly OnlineStoreContext _context;
        IOrderCategoriesServise _orderCategoriesServise;

        public CategoriesController(OnlineStoreContext context, IOrderCategoriesServise orderCategoriesServise)
        {
            _context = context;
            _orderCategoriesServise = orderCategoriesServise;
        }

        // GET: Categories
        //public async Task<IActionResult> Index(int level = 1)
        //{
        //    var categories = _context.Categories
        //        .Include(c => c.ParentCategory)
        //        .Include(c => c.CategoryProducts)
        //            .ThenInclude(cp => cp.Product)
        //            .Where(c => c.CategoryLevel == level);

        //    return View(await categories.ToListAsync());
        //}

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.Categories)
                .Include(c => c.CategoryProducts)
                    .ThenInclude(cp => cp.Product)
                .ToListAsync();

            if (categories.Count() == 0)
            {
                return View(categories);
            }

            var sortedCategories = _orderCategoriesServise.GetOrderCategories(categories);
            return View(sortedCategories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id, int? parentCategoryId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            if(parentCategoryId != null)
            {
                _context.Entry<Category>(category).Reference(c => c.ParentCategory).Load();
            }

            return View(category);
        }




        // GET: Categories/Create
        public IActionResult Create(int? id, int categoryLevel = 0)
        {
            Category category = new Category { ParentCategoryId = id, CategoryLevel = categoryLevel + 1 };
            CategoryViewModel categoryViewModel = new CategoryViewModel { Category = category };
            return View(categoryViewModel);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if(model.Image == null)
            {
                ModelState.AddModelError("Image", "No image have been chosen");
            }

            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Title = model.Category.Title,
                    CategoryLevel = model.Category.CategoryLevel,
                    Order = model.Category.Order,
                    ParentCategoryId = model.Category.ParentCategoryId
                };

                byte[] data = null;
                using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)model.Image.Length);
                    category.Image = data;
                }

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }




        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            //IFormFile image = new FormFile(new MemoryStream(category.Image), 0, category.Image.Length, "Image", "Image");
            CategoryViewModel model = new CategoryViewModel
            {
                Category = category,
                //Image = image
            };
            return View(model);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryViewModel model)
        {
            if (id != model.Category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(model.Image != null)
                    {
                        byte[] data = null;
                        using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)model.Image.Length);
                            model.Category.Image = data;
                        }
                    }


                    _context.Update(model.Category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(model.Category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }




        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            //var category = await _context.Categories.Include(cp => cp.CategoryProducts).FirstOrDefaultAsync(c => c.Id == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
