using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Services
{
    public interface IOrderCategoriesServise
    {
        public List<Category> GetOrderCategories(List<Category> categories);
    }

    public class OrderCategoriesServise : IOrderCategoriesServise
    {
        public OrderCategoriesServise() { }

        public List<Category> GetOrderCategories(List<Category> categories)
        {
            var sortedCategories = new List<Category>();

            //var maxLevel = categories.Max(c => c.CategoryLevel);
            int allMainCategory = categories.Where(c => c.CategoryLevel == 1).Count();
            List<Category> mainCategories = categories
                .Where(c => c.CategoryLevel == 1)
                .OrderBy(c => c.Order)
                .ToList();
            int level = 1;

            for (int i = 0; i < allMainCategory; i++)
            {
                sortedCategories.Add(mainCategories[i]);

                if (mainCategories[i].Categories != null)
                {
                    level++;
                    List<Category> subCatLewelTwo = new List<Category>(mainCategories[i].Categories.OrderBy(c2 => c2.Order));

                    for (int j = 0; j < subCatLewelTwo.Count(); j++)
                    {
                        sortedCategories.Add(subCatLewelTwo[j]);

                        if (subCatLewelTwo[j].Categories != null)
                        {
                            level++;
                            List<Category> subCatLewelThree = new List<Category>(subCatLewelTwo[j].Categories.OrderBy(c3 => c3.Order));

                            for (int k = 0; k < subCatLewelThree.Count(); k++)
                            {
                                sortedCategories.Add(subCatLewelThree[k]);
                            }
                        }
                    }
                }

                level = 1;
            }

            return sortedCategories;
        }
    }
}
