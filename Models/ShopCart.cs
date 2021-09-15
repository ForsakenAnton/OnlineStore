using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class ShopCart
    {
        private static List<CartItem> CartItems;// = new List<CartItem>();
        static ShopCart()
        {
            CartItems = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            CartItem cartItem = CartItems
                .Where(item => item.Product.Id == product.Id)
                .FirstOrDefault();
            
            if(cartItem == null)
            {
                CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            CartItems.RemoveAll(item => item.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return CartItems.Sum(item => (item.Product.Price - item.Product.Discount) * item.Quantity);
        }

        public void Clear()
        {
            CartItems.Clear();
        }

        public IEnumerable<CartItem> GetCartItems
        {
            get => CartItems;
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }


    //public class ShopCart
    //{
    //    private readonly OnlineStoreContext _context;
    //    public ShopCart(OnlineStoreContext context)
    //    {
    //        _context = context;
    //    }

    //    public string Id { get; set; } // Guid generation Id
    //    public ICollection<ShopCartItem> ShopCartItems { get; set; }


    //    public static ShopCart GetCart(IServiceProvider services)
    //    {
    //        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
    //        var context = services.GetService<OnlineStoreContext>();
    //        string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

    //        session.SetString("CartId", shopCartId);

    //        return new ShopCart(context)
    //        {
    //            Id = shopCartId
    //        };
    //    }

    //    public async Task AddToCartAsync(Product product)
    //    {
    //        _context.ShopCartItems.Add(new ShopCartItem
    //        {
    //            ShopCartId = Id,
    //            Product = product,
    //            Quantity = 1
    //        });

    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task<List<ShopCartItem>> GetShopCartItemsAsync()
    //    {
    //        return await _context.ShopCartItems
    //            .Include(i => i.Product)
    //            .Where(i => i.ShopCartId == Id)
    //            .ToListAsync();
    //    }
    //}
}
