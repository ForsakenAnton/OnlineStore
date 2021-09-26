using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using OnlineStore.Models.ViewModels;
using OnlineStore.Services;
using OnlineStore.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly OnlineStoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public ShopCartController(OnlineStoreContext context, UserManager<User> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public IActionResult Index(string emptyShopCart, string returnUrl)
        {
            ViewBag.emptyShopCart = emptyShopCart;
            ViewBag.returnUrl = returnUrl;
            return View(GetShopCart());
        }

        public async Task<IActionResult> AjaxAddToCart(int? productId)
        {
            if(productId == null)
            {
                return NoContent();
            }

            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if(product != null)
            {
                GetShopCart().AddItem(product, 1);

                return GetJson((int)productId);
            }

            return NoContent();
        }

        public async Task<IActionResult> AjaxRemoveFromCart(int? productId, int? quantity)
        {
            if (productId == null)
            {
                return NoContent();
            }

            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null && quantity != null)
            {
                GetShopCart().RemoveItem(product, (int)quantity);

                return GetJson((int)productId);
            }

            else if (product != null)
            {
                GetShopCart().RemoveAllSameItems(product);

                return GetJson((int)productId);
            }

            return NoContent();
        }


            [Authorize]
        public async Task<IActionResult> OrderPlacement(string pathAndQuery)
        {
            if(GetShopCart().GetCartItems.Count() == 0)
            {
                ViewBag.emptyShopCart = "Your shop cart is empty!";
                return RedirectToAction("Index", new { emptyShopCart = ViewBag.emptyShopCart });
            }

            User user = await _userManager.GetUserAsync(User);
            int quantityOrdersOfUser = _context.Orders
                .Include(o => o.User)
                .Where(o => o.User.Id == user.Id)
                .Count() + 1;

            OrderDataUserViewModel orderDataUserViewModel = new()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };
            ShopCart shopCart = GetShopCart();

            OrderPlacementViewModel viewModel = new()
            {
                ShopCart = shopCart,
                OrderDataUser = orderDataUserViewModel,
                Delivery = new Delivery(),
                QuantityOrdersOfUser = quantityOrdersOfUser,
                ReturnUrl = pathAndQuery
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OrderPlacement(OrderPlacementViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);
                ShopCart shopCart = GetShopCart();

                user.Name = viewModel.OrderDataUser.Name;
                user.Surname = viewModel.OrderDataUser.Surname;
                user.Email = viewModel.OrderDataUser.Email;

                Order order = new()
                {
                    OrderNumber = Guid.NewGuid(),
                    DateOfOrder = DateTime.Now,
                    State = State.None,
                    TotalPrice = shopCart.GetCartItems.Sum(item => (item.Product.Price - item.Product.Discount) * item.Quantity),
                    User = user
                };

                viewModel.Delivery.Order = order;

                List<OrderProduct> orderProducts = new List<OrderProduct>();
                foreach (var item in shopCart.GetCartItems)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.Product.Id); // (!)

                    orderProducts.Add(
                        new OrderProduct
                        {
                            Quantity = item.Quantity,
                            PriceOfOneProduct = product.Price - product.Discount,
                            Order = order,
                            Product = product 
                            // Product = item.Product (ef core Cannot insert explicit value for identity column in table 'Product' when IDENTITY_INSERT is set to OFF)
                            // короче нет BindingModel и контекст не отслеживается. (!) Отследил выше 
                        });
                }

                await _context.Deliveries.AddAsync(viewModel.Delivery);
                await _context.OrderProducts.AddRangeAsync(orderProducts);
                await _context.SaveChangesAsync();

                shopCart.Clear();

                var callbackUrl = Url.Action(
                        "Index",
                        "Home",
                        null,
                        protocol: HttpContext.Request.Scheme);

                foreach (var admin in await _userManager.GetUsersInRoleAsync("admin"))
                {
                    await _emailService.SendAsync("from_buyer@example.com", admin.Email, $"Got an order from user {user.UserName}",
                        $"To see order №{order.OrderNumber} <a href='{callbackUrl}'>follow the link</a>");
                }

                return RedirectToAction("Index", "Home");
            }

            viewModel.ShopCart = GetShopCart();
            return View(viewModel);
        }

        //[Authorize]
        //public async Task<IActionResult> AjaxEditOrderDataUser(string userId)
        //{
        //    User user = await _userManager.GetUserAsync(User);
        //    if(userId != user.Id)
        //    {
        //        return NoContent();
        //    }

        //    OrderDataUserViewModel viewModel = new()
        //    {
        //        Id = user.Id,
        //        Name = user.Name,
        //        Surname = user.Surname,
        //        Email = user.Email
        //    };

        //    return PartialView("_EditOrderDataUser", viewModel);
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<PartialViewResult> AjaxPostEditOrderDataUser([FromBody] OrderDataUserViewModel viewModel)
        //{
        //    User user = await _userManager.GetUserAsync(User);

        //    if (ModelState.IsValid)
        //    {
        //        user.Name = viewModel.Name;
        //        user.Surname = viewModel.Surname;
        //        user.Email = user.Email;

        //        await _userManager.UpdateAsync(user);

        //        ViewBag.successChangesDataUser = "The changes have been saved";

        //        return PartialView("_OrderDataUserInfo", viewModel);
        //    }

        //    ViewBag.failedChangesDataUser = "The changes have not been saved!";
        //    return PartialView("_EditOrderDataUser", viewModel);
        //}




        public ShopCart GetShopCart()
        {
            ShopCart shopCart = HttpContext.Session.Get<ShopCart>("ShopCart");
            if (shopCart == null)
            {
                shopCart = new ShopCart();
                HttpContext.Session.Set<ShopCart>("ShopCart", shopCart);
            }

            return shopCart;
        }



        private JsonResult GetJson(int productId)
        {
            int countAllProducts = GetShopCart().GetCartItems.Sum(i => i.Quantity);
            int countCurrentProduct = GetShopCart().GetCartItems.Where(i => i.Product.Id == productId).Sum(i => i.Quantity);
            decimal price = GetShopCart().GetCartItems.Where(i => i.Product.Id == productId).Sum(i => i.Product.Price * i.Quantity);
            decimal discount = GetShopCart().GetCartItems.Where(i => i.Product.Id == productId).Sum(i => i.Product.Discount * i.Quantity);
            decimal priceWithDiscount = price - discount;

            decimal priceTotal = GetShopCart().GetCartItems.Sum(i => i.Product.Price * i.Quantity);
            decimal discountTotal = GetShopCart().GetCartItems.Sum(i => i.Product.Discount * i.Quantity);
            decimal priceWithDiscountTotal = priceTotal - discountTotal;

            return Json(new
            {
                isSuccess = true,
                countAllProducts,
                countCurrentProduct,
                price,
                discount,
                priceWithDiscount,
                priceTotal,
                discountTotal,
                priceWithDiscountTotal
            });
        }
    }


    //public class ShopCartController : Controller
    //{
    //    private readonly ShopCart _shopCart;
    //    private readonly OnlineStoreContext _context;
    //    public ShopCartController(ShopCart shopCart, OnlineStoreContext context)
    //    {
    //        _shopCart = shopCart;
    //        _context = context;
    //    }

    //    public async Task<IActionResult> Index()
    //    {
    //        var items = await _shopCart.GetShopCartItemsAsync();
    //        _shopCart.ShopCartItems = items;

    //        ShopCartViewModel viewModel = new ShopCartViewModel
    //        {
    //            ShopCart = _shopCart
    //        };

    //        return View(viewModel);
    //    }

    //    public async Task<IActionResult> AddToCart(int id)
    //    {
    //        var item = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    //        if(item != null)
    //        {
    //            await _shopCart.AddToCartAsync(item);
    //        }

    //        return RedirectToAction("Index");
    //    }
    //}
}
