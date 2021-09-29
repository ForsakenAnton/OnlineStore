using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using OnlineStore.Models.IdentityModels.ViewModels;
using OnlineStore.Models.ViewModels;
using OnlineStore.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class MyCabinetController : Controller
    {
        private readonly OnlineStoreContext _context;
        private readonly UserManager<User> _userManager;

        public MyCabinetController(OnlineStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.pageId = 1;
            ViewBag.user = await _userManager.GetUserAsync(User);

            return View(GetShopCart());
        }

        public async Task<IActionResult> RemoveProductFromCart(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if(product != null)
            {
                GetShopCart().RemoveAllSameItems(product);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PersonalInformation(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            ViewBag.pageId = 2;
            return View(user);
        }

        public async Task<IActionResult> EditProfile(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return NoContent();
            }

            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            ChangePersonalInfoViewModel viewModel = new ChangePersonalInfoViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Lastname = user.Lastname,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email
            };

            return PartialView("_EditProfile", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditedProfile(string id, ChangePersonalInfoViewModel model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            ViewBag.pageId = 2;

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                //_context.Entry<User>(user).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                user.UserName = model.UserName;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Lastname = model.Lastname;
                user.DateOfBirth = model.DateOfBirth;
                user.Email = model.Email;

                await _userManager.UpdateAsync(user);             
                
                ViewBag.successChanges = "The changes have been saved";
                return View("PersonalInformation", user);
            }

            string failedChanges = "The changes did not saved\n\n";

            foreach (ModelError error in ViewData.ModelState.Values.SelectMany(v => v.Errors))
            {
                failedChanges += " - " + error.ErrorMessage + "\n";
            }
            failedChanges = failedChanges.TrimEnd('\n');
            ViewBag.failedChanges = failedChanges.Split("\n");

            return View("PersonalInformation", user);
        }

        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);

            List<MyOrdersViewModel> myOrdersViewModel = await _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                    .ThenInclude(o => o.Delivery)
                .GroupBy(op => op.Order.Id)
                .Select(g => new MyOrdersViewModel
                {
                    OrderId = g.Key, 
                    Order = _context.Orders.FirstOrDefault(o => o.Id == g.Key)
                })
                .ToListAsync();

            foreach (var group in myOrdersViewModel)
            {
                group.Products = await _context.OrderProducts
                    .Where(op => op.Order.Id == group.OrderId)
                    .Select(p => p.Product)
                    .ToListAsync();

                await _context.OrderProducts.Where(op => op.OrderId == group.OrderId).LoadAsync();
            }

            ViewBag.pageId = 3;
            return View(myOrdersViewModel);
        }



        public IActionResult ViewedProducts()
        {
            ViewBag.pageId = 4;
            return View();
        }

        public async Task<IActionResult> FavoriteProducts(string userId)
        {
            if(String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(u => u.FavoriteProducts)
                    .ThenInclude(fp => fp.Product)
                        .ThenInclude(P => P.Comments)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Id == userId);

            ViewBag.pageId = 5;
            return View(user);
        }

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
    }
}
