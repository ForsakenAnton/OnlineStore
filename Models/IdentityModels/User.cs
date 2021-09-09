using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.IdentityModels
{
    //[Keyless]
    public class User: IdentityUser
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Lastname { get; set; }

        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}
