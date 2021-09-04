using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components.LastViewedProducts
{
    public class LastViewedProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<Product> sessionProducts = new List<Product>();
            foreach (var key in HttpContext.Session.Keys.Where(k => k.Contains("haveBeenViewedProducts")))
            {
                sessionProducts.Add(HttpContext.Session.Get<Product>(key));
            }

            return View("_LastViewedProducts", sessionProducts);
        }
    }
}
