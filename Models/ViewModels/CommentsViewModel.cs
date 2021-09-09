using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class CommentsViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public Product Product { get; set; }
        //public int? ProductId { get; set; }
        public IEnumerable<FavoriteProduct> FavoriteProducts { get; set; }
        public User User { get; set; }
    }
}
