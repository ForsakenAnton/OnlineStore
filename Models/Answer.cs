using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsModerated { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
