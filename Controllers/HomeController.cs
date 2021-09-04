using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Sessions;
using OnlineStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Models.IdentityModels;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;
        public IConfiguration _configuration;
        private readonly OnlineStoreContext _context;
        IOrderCategoriesServise _orderCategoriesServise;

        public HomeController(UserManager<User> userManager, ILogger<HomeController> logger, IConfiguration configuration, OnlineStoreContext context, IOrderCategoriesServise orderCategoriesServise)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _orderCategoriesServise = orderCategoriesServise;
        }

        public async Task<IActionResult> Index()
        {
            var manufacturers = await _context.Manufacturers.ToListAsync();

            var products = await _context.Products
                 .Include(p => p.CategoryProducts)
                 .ThenInclude(cp => cp.Category)
                 .ToListAsync();

            //var categories = await _context.Categories
            //    .Include(c => c.CategoryProducts)
            //    .Where(c => c.IsContainProducts == true)
            //    .ToListAsync();

            //var cp = await _context.CategoryProducts
            //     .Include(cp => cp.Category)
            //     .Include(cp => cp.Product)
            //     .ToListAsync();

            List<ProductsGroupByCategoryViewModel> productsGroupByCategory = await _context.CategoryProducts
                .Where(cp => cp.Category.IsTop == true)
                .GroupBy(cp => cp.Category.Title)
                .Select(g => new ProductsGroupByCategoryViewModel
                {
                    CategoryTitle = new Tuple<int, string>(_context.Categories.FirstOrDefault(c => c.Title == g.Key).Id, g.Key)
                    //Products = g.
                    //AsEnumerable()
                    //.Select(cp => cp.Product)
                    //.ToList()
                    // не могу сделать подзапрос (выше не та выборка продуктов что нужна, а только для теста). Делаю это конструкцией foreach ниже
                })
                .ToListAsync();
            foreach (var group in productsGroupByCategory)
            {
                //_logger.LogInformation(group.CategoryTitle);
                group.Products = _context.CategoryProducts
                    .Where(cp => cp.Category.Title == group.CategoryTitle.Item2)
                    .Select(p => p.Product)
                    .ToList();
            }

            IndexViewModel viewModel = new IndexViewModel
            {
                Manufacturers = manufacturers,
                Products = products,
                ProductsGroupByCategoryViewModel = productsGroupByCategory
            };

            return View(viewModel);
        }



        public async Task<IActionResult> IndexCategories(int? categoryId)
        {

            if (categoryId != null)
            {
                var category = await _context.Categories
                    .Include(c => c.Categories)
                    .Include(c => c.CategoryProducts)
                        .ThenInclude(cp => cp.Product)
                        .ThenInclude(p => p.Comments)
                    .FirstOrDefaultAsync(c => c.Id == categoryId);
                if (category == null)
                {
                    return NotFound();
                }

                if (category.IsContainSubCategories)
                {
                    return View("IndexCategories", category.Categories.OrderBy(c => c.Order));
                }
                else if (category.IsContainProducts)
                {
                    var selectIenumerableProducts = category.CategoryProducts
                        .Where(cp => cp.CategoryId == categoryId)
                        .Select(p => p.Product);

                    // to IQueryable
                    IQueryable<Product> products = _context.Products
                        .Where(p => selectIenumerableProducts.Contains(p))
                        .OrderBy(p => p.Price);

                    //IQueryable<Product> products = _context.Products
                    //    .Aggregate(new List<Product>(), (acc, dest) => acc.add);

                    int count = await products.CountAsync();
                    var items = await products.Skip(0).Take(4).ToListAsync();

                    IndexProductsViewModel viewModel = new IndexProductsViewModel
                    {
                        Products = items,
                        SortViewModel = new SortViewModel(SortState.PriceAsc),
                        FilterViewModel = new FilterViewModel(null, categoryId, null),
                        PageListViewModel = new PageViewModel<Product>(count, 1, 4),
                        ProductId = null,
                        User = await _userManager.GetUserAsync(User)
                    };


                    return View("IndexProducts", viewModel);
                }
                else
                {
                    //return Content("No products in category \"" + category.Title + "\"...");
                    return Content("This category is not contain any products");
                }
            }

            return NotFound();
        }

        public async Task<IActionResult> IndexProducts(int? productId, int? categoryId, string searchString, int page = 1, SortState sortOrder = SortState.PriceAsc)
        {
            int pageSize = 4;

            IQueryable<Product> products = _context.Products
                .Include(p => p.Manufacturer)
                .Include(p => p.Comments);
                    //.ThenInclude(c => c.User)
                //.ToListAsync();

            // Filter //////////////////////////////////////

            if (productId != null)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == productId);

                if (product != null)
                {

                    products = products
                       .Where(p => p.Title.ToLower().Trim().Contains(product.Title.ToLower().Trim()) ||
                                   product.Title.ToLower().Trim().Contains(p.Title.ToLower().Trim()));
                        //.ToList();
                }
            }
            else if (categoryId != null && categoryId != 0)
            {
                var selectIenumerableProducts = _context.CategoryProducts
                        .Where(cp => cp.CategoryId == categoryId)
                        .Select(p => p.Product);

                products = products.Where(p => selectIenumerableProducts.Contains(p));
            }


            /*else if !!!*/
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products
                    .Where(p => p.Title.ToLower().Trim().Contains(searchString) ||
                                p.Feature.ToLower().Trim().Contains(searchString));
                //.ToList();

                //page = 1;
            }

            // Sort /////////////////////////////////////////////

            products = sortOrder switch
            {
                SortState.PriceAsc => products.OrderBy(p => (p.Price - p.Discount)),
                SortState.PriceDesc => products.OrderByDescending( p => (p.Price - p.Discount)),
                _ => products.OrderBy(p => p.Price)
            };

            // Pagination ///////////////////////////////////////

            //PageViewModel<Product> pageListViewModel = await PageViewModel<Product>.CreateAsync(products, page, pageSize);
            int count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexProductsViewModel viewModel = new IndexProductsViewModel
            {
                Products = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(productId, categoryId, searchString),
                PageListViewModel = new PageViewModel<Product>(count, page, pageSize),
                ProductId = productId,
                User = await _userManager.GetUserAsync(User)
            };

            return View("IndexProducts", viewModel);
        }
    








        public async Task<IActionResult> GetAllCategories()
        {
            var firstLevelcategories = await _context.Categories
                .Where(c => c.CategoryLevel == 1)
                .OrderBy(c => c.Order)
                .ToListAsync();

            return View("IndexCategories", firstLevelcategories);
        }




        //public async Task<IActionResult> Search(string title)
        //{
        //    var products = await _context.Products
        //        .Where(p => p.Title.ToLower().Contains(title.ToLower()) || title.ToLower().Contains(p.Title.ToLower()))
        //        .ToListAsync();
        //    return View("IndexProducts", products);
        //}


        public async Task<PartialViewResult> AjaxMainSearchPartial(string searchString)
        {
            var products = new List<Product>();
            AjaxMainSeachViewModel viewModel;

            if (String.IsNullOrEmpty(searchString))
            {
                products = await _context.Products
                .OrderBy(p => p.IsTop)
                .Take(5)
                .ToListAsync();

                var mainCategories = await _context.Categories
                    .Where(cp => cp.CategoryProducts.Count() != 0)
                    .Take(5)
                    .ToListAsync();

                viewModel = new AjaxMainSeachViewModel
                {
                    Products = products,
                    Categories = mainCategories,
                    Tags = new List<string>()
                };

                return PartialView("_AjaxMainSearchPartial", viewModel);
            }


            products = await _context.Products
                .Include(cp => cp.CategoryProducts)
               .Where(p => p.Title.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                           searchString.ToLower().Trim().Contains(p.Title.ToLower().Trim()) ||
                           p.Feature.ToLower().Trim().Contains(searchString.ToLower().Trim()) ||
                           searchString.ToLower().Trim().Contains(p.Feature.ToLower().Trim()))
               .OrderBy(p => p.IsTop)
               .Take(5)
               .ToListAsync();

            var categoryProducts = await _context.CategoryProducts
                .Include(cp => cp.Product)
                .Include(cp => cp.Category)
                .Where(cp => products.Contains(cp.Product))
                .ToListAsync();

            var categories = categoryProducts
                .Where(cp => categoryProducts.Contains(cp))
                .Select(c => c.Category)
                .Distinct()
                ;


            List<string> tags = new List<string>(5);

            foreach (var product in await _context.Products.ToListAsync())
            {
                string[] titleSplit = product.Title.ToLower().Trim().Split(' ').Except(tags).ToArray();
                string[] featureSplit = product.Feature.ToLower().Trim().Split(' ').Except(tags).ToArray();
                string[] tagsSplit = titleSplit.Concat(featureSplit).ToArray();

                string tag = tagsSplit.FirstOrDefault(ts => ts.Contains(searchString.ToLower().Trim()) /* || searchString.ToLower().Trim().Contains(ts) */);
                if (tag != null)
                {
                    if(!tags.Contains(tag))
                        tags.Add(tag);

                    if (tags.Count == 5)
                        break;

                    continue;
                }
            }

           viewModel = new AjaxMainSeachViewModel
            {
                Products = products,
                Categories = categories,
                Tags = tags
            };

            return PartialView("_AjaxMainSearchPartial", viewModel);
        }




        public IActionResult Details(int? productId, int agaxPageId = 1)
        {
            if (productId == null)
            {
                return NotFound();
            }

            ViewBag.agaxPageId = agaxPageId;
            ViewBag.commentsCount = _context.Comments.Count(c => c.ProductId == productId && c.IsModerated == true);

            return View(productId);
        }


        public async Task<IActionResult> AllAboutProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.Answers)
                        .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }


            HttpContext.Session.Set<Product>("haveBeenViewedProducts" + product.Id, product);

            return PartialView("_AllAboutProduct", product);
        }

        public async Task<PartialViewResult> Сharacteristics(int? id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            return PartialView("_Сharacteristics");
        }

        public async Task<IActionResult> Comments(int? id, string sort)
        {
            if (String.IsNullOrEmpty(sort))
                sort = "byDate";

            ViewData["sortParams"] = sort == "byDate" ? "By date" : "By useful";

            var comments = await _context.Comments
                .Include(c => c.Likes)
                .Include(c => c.User)
                .Include(c => c.Product)
                .Include(c => c.Answers)
                    .ThenInclude(a => a.User)
                .Where(c => c.Product.Id == id && c.IsModerated)
                .ToListAsync();

            switch (sort)
            {
                case "byDate":
                    comments = comments.OrderByDescending(c => c.Date).ToList();
                    break;
                case "byUseful":
                    comments = comments.OrderByDescending(c => c.Likes.Count(l => l.Liking) - c.Likes.Count(l => l.Unliking)).ToList();
                    break;
                default:

                    break;
            }

            //var user = await _userManager.GetUserAsync(User);
            //_logger.LogInformation(user?.UserName);
            //_logger.LogInformation(User.IsInRole("Admin").ToString());
            //_logger.LogInformation(User.Identity.Name);

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            ViewBag.product = product;


            return PartialView("_Comments", comments);
        }


        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> LikingAsync(int? commentId, bool thumbsUp, bool thumbsDown)
        {
            if (commentId == null)
                return NoContent();

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NoContent();

            Comment comment = await _context.Comments
                .Include(c => c.Likes)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            var likeToUpdate = await _context.Likes
                .FirstOrDefaultAsync(l => l.CommentId == comment.Id && l.User.Id == user.Id);
            if (likeToUpdate == null)
            {
                Like likeToCreate = new Like
                {
                    Liking = thumbsUp,
                    Unliking = thumbsDown,
                    Comment = comment,
                    User = user
                };
                _context.Likes.Add(likeToCreate);
                await _context.SaveChangesAsync();
            }
            else
            {
                if(thumbsUp)
                    likeToUpdate.Liking = !likeToUpdate.Liking;

                if(thumbsDown)
                    likeToUpdate.Unliking = !likeToUpdate.Unliking;

                _context.Likes.Update(likeToUpdate);
                await _context.SaveChangesAsync();
            }

            return PartialView("_Liking", comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SentFeedbackAsync(Comment comment)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                comment.User = user;
                comment.Date = DateTime.Now;
                comment.IsModerated = false; //true; // true, пока до админ части недобрался

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return PartialView("_SentFeedbackModal");
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SentAnswerAsync(Answer answer)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                answer.User = user;
                answer.Date = DateTime.Now;
                answer.IsModerated = false; // true, пока до админ части недобрался

                _context.Answers.Add(answer);
                await _context.SaveChangesAsync();

                return PartialView("_SentFeedbackModal");
            }
            return NoContent();
        }











        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
