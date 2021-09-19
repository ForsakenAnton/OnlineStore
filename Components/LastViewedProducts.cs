using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using OnlineStore.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components.LastViewedProducts
{
    public class LastViewedProductsViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly OnlineStoreContext _context;
        public LastViewedProductsViewComponent(UserManager<User> userManager, OnlineStoreContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> sessionProducts = new List<Product>();
            foreach (var key in HttpContext.Session.Keys.Where(k => k.Contains("haveBeenViewedProducts")))
            {
                sessionProducts.Add(HttpContext.Session.Get<Product>(key));
            }

            var user = await _userManager.GetUserAsync((System.Security.Claims.ClaimsPrincipal)User);
            IEnumerable<FavoriteProduct> favoriteProducts = null;
            if (user != null)
            {
                favoriteProducts = await _context.FavoriteProducts
                   .Include(fp => fp.Product)
                   .Include(fp => fp.User)
                   .Where(fp => fp.User.Id == user.Id).ToListAsync();
            }

            ViewBag.user = user;
            ViewBag.favoriteProducts = favoriteProducts;
            ViewBag.ShopCart = HttpContext.Session.Get<ShopCart>("ShopCart");

            return View("_LastViewedProducts", sessionProducts);
        }
    }
}
