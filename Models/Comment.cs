using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        //[Required]
        //[MinLength(1)]
        public string Message { get; set; }
        public string Virtues { get; set; }
        public string Shortcomings { get; set; }

        //[Range(1, 5)]
        public int Rating { get; set; }
        public bool IsModerated { get; set; }
        public DateTime Date { get; set; }

        public int UserId { get; set; } // User: IdentityUser
        public int ProductId { get; set; }

        // Nav
        public User User { get; set; }
        public Product Product { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Answer> Answers { get; set; }


        //[ForeignKey("ParentComment")]
        //public int? ParentCommentId { get; set; }

        //public Comment ParentComment { get; set; }

        //public ICollection<Comment> Answers { get; set; }

    }
}
