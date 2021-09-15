using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;
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
        public ShopCartController(OnlineStoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
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

                int countProducts = GetShopCart().GetCartItems.Sum(i => i.Quantity);
                return Json(new { isAdded = true, countProducts });
            }

            return NoContent();
        }

        public async Task<IActionResult> AjaxRemoveFromCart(int? productId)
        {
            if (productId == null)
            {
                return NoContent();
            }

            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                GetShopCart().RemoveItem(product);

                return Json(new { isRemoved = true });
            }

            return NoContent();
        }



        public ShopCart GetShopCart()
        {
            ShopCart shopCart = HttpContext.Session.Get<ShopCart>("ShopCart");
            if(shopCart == null)
            {
                shopCart = new ShopCart();
                HttpContext.Session.Set<ShopCart>("ShopCart", shopCart);
            }

            return shopCart;
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
