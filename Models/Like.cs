using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Like
    {
        public int Id { get; set; }
        public bool IsLike { get; set; }
        public bool IsUnlike { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public bool Liking
        {
            get => IsLike;
            set
            {
                if (value == true)
                {
                    IsUnlike = false;
                }
                IsLike = value;
            }
        }

        public bool Unliking
        {
            get => IsUnlike;
            set
            {
                if (value == true)
                {
                    IsLike = false;
                }
                IsUnlike = value;
            }
        }

    }
}
