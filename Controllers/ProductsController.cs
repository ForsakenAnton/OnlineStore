﻿using System;
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
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly OnlineStoreContext _context;
        IOrderCategoriesServise _orderCategoriesServise;
        public ProductsController(OnlineStoreContext context, IOrderCategoriesServise orderCategoriesServise)
        {
            _context = context;
            _orderCategoriesServise = orderCategoriesServise;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(cp => cp.CategoryProducts)
                    .ThenInclude(c => c.Category)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                        .ThenInclude(l => l.Likes)
                .ToListAsync();

            return View(products);
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.CategoryProducts)
                    .ThenInclude(cp => cp.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        // GET: Products/Create
        public IActionResult Create()
        {
            var categories = _context.Categories.Where(c => c.Categories.Count() == 0);
            ProductViewModel model = new ProductViewModel
            {
                Product = new Product(),
                ManufacturersList = new SelectList(_context.Manufacturers, "Id", "Title"),
                CategoriesList = new SelectList(categories, "Id", "Title")
            };
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (model.Image == null)
            {
                ModelState.AddModelError("Image", "No image have been chosen");
            }

            if (ModelState.IsValid)
            {
                byte[] data = null;
                using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)model.Image.Length);
                    model.Product.Image = data;
                }

                if (model.categoriesId.Length != 0)
                {
                    for(int i = 0; i < model.categoriesId.Length; i++)
                    {
                        Category category = await _context.Categories.FindAsync(model.categoriesId[i]);
                        CategoryProduct categoryProduct = new CategoryProduct
                        {
                            Product = model.Product,
                            Category = category
                        };
                        _context.Add(categoryProduct);
                    }
                }

                else
                {
                    _context.Add(model.Product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = _context.Categories.Where(c => c.Categories.Count() == 0);
            ProductViewModel invalidModel = new ProductViewModel
            {
                Product = model.Product,
                ManufacturersList = new SelectList(_context.Manufacturers, "Id", "Title", model.Product.ManufacturerId),
                CategoriesList = new SelectList(categories, "Id", "Title", model.categoriesId)
            };
            return View(invalidModel);
        }



        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // все категории
            var categories = await _context.Categories.
                Include(c => c.CategoryProducts).
                Where(c => c.Categories.Count() == 0) // где нет подкатегорий
                .ToListAsync();

            // выбранные категории у продукта 
            List<CategoryProduct> selectedCategories = await _context.CategoryProducts
                .Where(cp => cp.ProductId == id)
                .Distinct() // убираем повторяшки
                .ToListAsync();

            List<int> selectedId = new List<int>();

            for (int i = 0; i < selectedCategories.Count(); i++)
            {
                selectedId.Add(selectedCategories[i].CategoryId);
            }

            int[] selectedIdToArray = selectedId.ToArray();

            ProductViewModel model = new ProductViewModel
            {
                Product = product,
                ManufacturersList = new SelectList(_context.Manufacturers, "Id", "Title", product.ManufacturerId),
                CategoriesList = new SelectList(categories, "Id", "Title", selectedIdToArray),
                categoriesId = selectedIdToArray,
                oldCategoriesId = selectedIdToArray
            };

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model) // oldCategoriesId - старые значения в БД
        {
            if (id != model.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Image != null)
                    {
                        byte[] data = null;
                        using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)model.Image.Length);
                            model.Product.Image = data;
                        }
                    }

                    // СРАВНИВАЕМ СТАРЫЕ И НОВЫЕ КАТЕГОРИИ
                    bool isEquals = false;
                    if (model.categoriesId != null && model.oldCategoriesId != null)
                    {
                        isEquals = model.categoriesId.SequenceEqual(model.oldCategoriesId);
                    }
                    if (isEquals == false &&  model.oldCategoriesId != null)
                    {
                        foreach (var oldId in model.oldCategoriesId)
                        {
                            CategoryProduct categoryProductToDelete = await _context.CategoryProducts.FirstOrDefaultAsync(cp => cp.CategoryId == oldId && cp.ProductId == model.Product.Id);
                            _context.Remove(categoryProductToDelete);
                            _context.Entry<CategoryProduct>(categoryProductToDelete).State = EntityState.Deleted;
                            //await _context.SaveChangesAsync();
                        }
                        //await _context.SaveChangesAsync();
                    }

                    if (isEquals == false && model.categoriesId != null)
                    {

                        for (int i = 0; i < model.categoriesId.Length; i++)
                        {
                            Category category = await _context.Categories.FindAsync(model.categoriesId[i]);
                            CategoryProduct categoryProduct = new CategoryProduct
                            {
                                Product = model.Product,
                                Category = category
                            };
                            _context.Update(categoryProduct);
                        }
                    }

                    else
                    {
                        _context.Update(model.Product);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Product.Id))
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

            var categories = _context.Categories.Where(c => c.Categories.Count() == 0);
            ProductViewModel invalidModel = new ProductViewModel
            {
                Product = model.Product,
                ManufacturersList = new SelectList(_context.Manufacturers, "Id", "Title", model.Product.ManufacturerId),
                CategoriesList = new SelectList(categories, "Id", "Title", model.categoriesId)
            };

            return View(invalidModel);
        }



        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include( p => p.Manufacturer)
                .Include(p => p.CategoryProducts)
                    .ThenInclude(cp => cp.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        //public IEnumerable<Category> GetCategories(Predicate<Category> selector)
        //{

        //}
        //public IEnumerable<Category> Include(Expression<Func<Category, TProperty>> selector)
        //{

        //}

        // Predicate<Category>


    }
}