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
using AutoMapper;
using OnlineStore.Models.ModelsDTO;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<HomeController> _logger;
        public IConfiguration _configuration;
        private readonly OnlineStoreContext _context;
        IOrderCategoriesServise _orderCategoriesServise;
        private readonly IMapper _mapper;

        public HomeController(
            UserManager<User> userManager,
            ILogger<HomeController> logger,
            IConfiguration configuration,
            OnlineStoreContext context,
            IOrderCategoriesServise orderCategoriesServise,
            IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _orderCategoriesServise = orderCategoriesServise;
            _mapper = mapper;
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
                    CategoryTitle = new Tuple<int, string>(_context.Categories.FirstOrDefault(c => c.Title == g.Key).Id, g.Key),
                    
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
                        .OrderBy(p => (p.Price - p.Discount));

                    //IQueryable<Product> products = _context.Products
                    //    .Aggregate(new List<Product>(), (acc, dest) => acc.add);



                    int count = await products.CountAsync();
                    var items = await products.Skip(0).Take(8).ToListAsync();

                    IndexProductsViewModel viewModel = new IndexProductsViewModel
                    {
                        Products = items,
                        SortViewModel = new SortViewModel(SortState.PriceAsc),
                        FilterViewModel = new FilterViewModel(null, null, categoryId, null, null, null),
                        PageListViewModel = new PageViewModel(count, 1, 8),
                        ProductId = null,
                        //User = user,
                        //FavoriteProducts = favoriteProducts
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
            int pageSize = 8;

            IQueryable<Product> products = _context.Products
                .Include(p => p.Characteristic)
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


            // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) //

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<Characteristic> listCharacteristics = await products.Select(p => new Characteristic
            {
                SerializedCharactetistics = p.Characteristic.SerializedCharactetistics
            })
            .ToListAsync();

            List<CharacteristicsListDto> characteristicsListDto = _mapper.Map<List<CharacteristicsListDto>>(listCharacteristics);

            //var groupingListOfCharacteristics = characteristicsListDto
            //    .GroupBy(c => c.Id)
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) // (!) //

            IndexProductsViewModel viewModel = new IndexProductsViewModel
            {
                Products = items,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(productId, searchString, categoryId, null, null, null),
                PageListViewModel = new PageViewModel(count, page, pageSize),
                ProductId = productId,
                //User = user,
                //FavoriteProducts = favoriteProducts
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




        public IActionResult Details(int? productId, int page = 1, SortState sortOrder = SortState.ByDate, int ajaxPageId = 1)
        {
            if (productId == null)
            {
                return NotFound();
            }

            // Dynamic types for view ////////////////////////////////////////////////////
            ViewBag.page = page;
            ViewBag.sortOrder = sortOrder;
            ViewBag.agaxPageId = ajaxPageId;
            ViewBag.commentsCount = _context.Comments.Count(c => c.ProductId == productId && c.IsModerated == true);
            //////////////////////////////////////////////////////

            return View(productId);
        }


        public async Task<IActionResult> AllAboutProduct(int? id)
        {
            if (id == null)
            {
                return NoContent();
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
                return NoContent();
            }

            HttpContext.Session.Set<Product>("haveBeenViewedProducts" + product.Id, product);

            return PartialView("_AllAboutProduct", product);
        }

        public async Task<IActionResult> Сharacteristics(int? id)
        {
            if(id == null)
            {
                return NoContent();
            }

            var characteristic = await _context.Characteristics
                .FirstOrDefaultAsync(ch => ch.ProductId == id);

            //if(characteristic == null)
            //{
            //    return NoContent();
            //}

            CharacteristicsListDto characteristicsListDto = _mapper.Map<CharacteristicsListDto>(characteristic);

            return PartialView("_Сharacteristics", characteristicsListDto);
        }

        public async Task<IActionResult> Comments(int? id, int page = 1, SortState sortOrder = SortState.ByDate)
        {
            if(id == null)
            {
                return NoContent();
            }

            int pageSize = 4;

            IQueryable<Comment> comments = _context.Comments
                .Include(c => c.Likes)
                .Include(c => c.User)
                .Include(c => c.Product)
                .Include(c => c.Answers
                               .Where(a => a.IsModerated == true))
                    .ThenInclude(a => a.User)
                .Where(c => c.Product.Id == id && c.IsModerated);

            //comments = 

            switch (sortOrder)
            {
                case SortState.ByDate:
                    comments = comments.OrderByDescending(c => c.Date);
                    break;

                case SortState.ByUseful:
                    comments = comments.OrderByDescending(c => c.Likes.Count(l => l.Liking) - c.Likes.Count(l => l.Unliking));
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


            int count = await comments.CountAsync();
            var items = await comments.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            CommentsViewModel viewModel = new CommentsViewModel
            {
                Comments = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                Product = product,
                //FavoriteProducts = favoriteProducts,
                //User = user
            };

            return PartialView("_Comments", viewModel);
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

        [Authorize]
        public async Task<IActionResult> FavoriteAsync(int? productId)
        {
            var user = await _userManager.GetUserAsync(User);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            FavoriteProduct favorite = new FavoriteProduct
            {
                Product = product,
                User = user
            };
            _context.FavoriteProducts.Add(favorite);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
