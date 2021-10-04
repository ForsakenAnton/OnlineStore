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
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.Sessions;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly OnlineStoreContext _context;
        IOrderCategoriesServise _orderCategoriesServise;

        public ShopCart ShopCart { get; private set; }

        public ProductsController(OnlineStoreContext context, IOrderCategoriesServise orderCategoriesServise)
        {
            _context = context;
            _orderCategoriesServise = orderCategoriesServise;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? categoryId, int? manufacturerId, string searchString, int page = 1, SortState sortOrder = SortState.PriceAsc)
        {
            int pageSize = 10;

            IQueryable<Product> products = _context.Products
                .Include(p => p.Manufacturer)
                .Include(cp => cp.CategoryProducts)
                    .ThenInclude(c => c.Category)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Answers)
                        .ThenInclude(a => a.User)
                .Include(p => p.Comments)
                        .ThenInclude(l => l.Likes);


            if (categoryId != null && categoryId != 0)
            {
                var selectIenumerableProducts = _context.CategoryProducts
                        .Where(cp => cp.CategoryId == categoryId)
                        .Select(p => p.Product);

                products = products.Where(p => selectIenumerableProducts.Contains(p));
            }

            if (manufacturerId != null && manufacturerId != 0)
            {
                products = products.Where(p => p.ManufacturerId == manufacturerId);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products
                    .Where(p => p.Title.ToLower().Trim().Contains(searchString) ||
                                p.Feature.ToLower().Trim().Contains(searchString));
            }

            var comments = _context.Comments
                .Include(c => c.Answers)
                .Where(c => products.Contains(c.Product) && c.IsModerated == false);

            var answers = _context.Answers
                .Where(a => comments.Contains(a.Comment));

            // Sort /////////////////////////////////////////////

            products = sortOrder switch
            {
                SortState.PriceAsc => products.OrderBy(p => p.Price),
                SortState.PriceDesc => products.OrderByDescending(p => p.Price),
                SortState.TopAsc => products.OrderBy(p => p.IsTop),
                SortState.TopDesc => products.OrderByDescending(p => p.IsTop),
                SortState.DiscountAsc => products.OrderBy(p => p.Discount),
                SortState.DiscountDesc => products.OrderByDescending(p => p.Discount),
                SortState.CountAsc => products.OrderBy(p => p.Count),
                SortState.CountDesc => products.OrderByDescending(p => p.Count),

                SortState.CommentAsc => products.OrderBy(p => p.Comments.Where(c => c.IsModerated == false).Count()),
                SortState.CommentDesc => products.OrderByDescending(p => p.Comments.Where(c => c.IsModerated == false).Count()),
                SortState.AnswerAsc => products.OrderBy(p => p.Comments.Count(c => c.Answers.Count != 0)),
                SortState.AnswerDesc => products.OrderByDescending(p => p.Comments.Count(c => c.Answers.Count != 0)),
                _ => products.OrderBy(p => p.Price)
            };

            // Pagination ///////////////////////////////////////

            //PageViewModel<Product> pageListViewModel = await PageViewModel<Product>.CreateAsync(products, page, pageSize);
            int count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // SelectListCategories
            var categories = await _context.Categories
                .Where(c => c.Categories.Count == 0)
                .ToListAsync();

            // SelectListManufacturers
            var manufacturers = await _context.Manufacturers.ToListAsync();


            IndexProductsViewModel viewModel = new IndexProductsViewModel
            {
                Products = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(null, searchString, categoryId, categories, manufacturerId, manufacturers),
                PageListViewModel = new PageViewModel(count, page, pageSize)
            };

            return View(viewModel);
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

            HttpContext.Session.Remove("haveBeenViewedProducts" + id);
            ShopCart shopCart = HttpContext.Session.Get<ShopCart>("ShopCart");
            shopCart.RemoveAllSameItems(product);

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
