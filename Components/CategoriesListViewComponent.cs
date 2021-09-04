using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Components
{
    public class CategoriesListViewComponent : ViewComponent
    {
        private readonly OnlineStoreContext _context;
        private readonly IOrderCategoriesServise _orderCategoriesServise;

        public CategoriesListViewComponent(OnlineStoreContext context, IOrderCategoriesServise orderCategoriesServise)
        {
            _context = context;
            _orderCategoriesServise = orderCategoriesServise;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.Categories)
                .Include(c => c.CategoryProducts)
                    .ThenInclude(cp => cp.Product)
                .ToListAsync();

            if (categories.Count() == 0)
            {
                return View("_CategoriesList", categories);
            }

            var sortedCategories = _orderCategoriesServise.GetOrderCategories(categories);
            return View("_CategoriesList", sortedCategories);
        }
    }
}
