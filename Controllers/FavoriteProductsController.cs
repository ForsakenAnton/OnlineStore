using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;

namespace OnlineStore.Controllers
{
    public class FavoriteProductsController : Controller
    {
        private readonly OnlineStoreContext _context;
        private readonly UserManager<User> _userManager;

        public FavoriteProductsController(OnlineStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> AddOrDeleteFavorite(int? productId)
        {
            if (productId == null)
                return NoContent();

            string userId = _userManager.GetUserId(User);

            if (String.IsNullOrEmpty(userId))
                return NoContent();

            var currentUser = await _context.Users
                .Include(u => u.FavoriteProducts)
                    .ThenInclude(fp => fp.Product)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (currentUser.FavoriteProducts.Any( fp => fp.Product.Id == productId))
            {
                var favorite = await _context.FavoriteProducts.FirstOrDefaultAsync(fp => fp.Product.Id == productId);
                _context.FavoriteProducts.Remove(favorite);
                await _context.SaveChangesAsync();

                var favoritesCount = await _context.FavoriteProducts
                    .CountAsync(fp => fp.User.Id == currentUser.Id);

                var jsonresult = new
                {
                    count = favoritesCount,
                    addToFavorite = false
                };

                return Json(jsonresult);
            }

            else
            {
                FavoriteProduct favoriteProduct = new FavoriteProduct
                {
                    Product = product,
                    User = currentUser
                };

                _context.FavoriteProducts.Add(favoriteProduct);
                await _context.SaveChangesAsync();

                var favoritesCount = await _context.FavoriteProducts
                    .CountAsync(fp => fp.User.Id == currentUser.Id);

                var jsonresult = new
                {
                    count = favoritesCount,
                    addToFavorite = true
                };
                return Json(jsonresult);
            }
        }
    }
}
