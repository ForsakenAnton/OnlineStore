using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommentsController : Controller
    {
        private readonly OnlineStoreContext _context;

        public CommentsController(OnlineStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProductCommentsList(int? productId)
        {
            if (productId == null)
                return NotFound();

            var comments = await _context.Comments
                .Include(c => c.User)
                    .ThenInclude(u => u.Likes)
                .Include(c => c.Answers)
                    .ThenInclude(a => a.User)
                .Include( c => c.Likes)
                .Include(c => c.Product)
                .Where(c => c.ProductId == productId)
                .OrderByDescending(c => c.Date)
                .ToListAsync();

            ViewBag.product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            return View(comments);
        }

        public async Task<IActionResult> AllowComment(int? commentId)
        {
            if (commentId == null)
                return NoContent();

            var comment = await _context.Comments
                .Include(c => c.User)
                    .ThenInclude(u => u.Likes)
                .Include(c => c.Answers)
                    .ThenInclude(a => a.User)
                .Include(c => c.Likes)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                return NoContent();

            comment.IsModerated = true;

            _context.Entry<Comment>(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            ViewBag.showAccordionClass = "show";
            return PartialView("_CommentItem", comment);
        }

        public async Task<IActionResult> ProhibitComment(int? commentId)
        {
            if (commentId == null)
                return NoContent();

            var comment = await _context.Comments
                .Include(c => c.User)
                    .ThenInclude(u => u.Likes)
                .Include(c => c.Answers)
                    .ThenInclude(a => a.User)
                .Include(c => c.Likes)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                return NoContent();

            comment.IsModerated = false;

            _context.Entry<Comment>(comment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            ViewBag.showAccordionClass = "show";
            return PartialView("_CommentItem", comment);
        }

        public async Task<IActionResult> DeleteComment(int? commentId)
        {
            if (commentId == null)
                return NoContent();

            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                return NoContent();

            _context.Entry<Comment>(comment).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            ViewBag.message = "Review was deleted";

            return PartialView("_DeletedAlert");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifiedFeedback(int id, Comment comment)
        {
            if (id != comment.Id)
                return NotFound();

            if(ModelState.IsValid)
            {
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();
                return PartialView("_ModifiedModal");
            }

            return NoContent();
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> AllowAnswer(int? answerId)
        {
            if (answerId == null)
                return NoContent();

            var answer = await _context.Answers
                .Include(a => a.User)
                    //.ThenInclude(u => u.Likes)
                .Include(a => a.Comment)               
                .FirstOrDefaultAsync(a => a.Id == answerId);

            if (answer == null)
                return NoContent();

            answer.IsModerated = true;

            _context.Entry<Answer>(answer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            ViewBag.showAccordionInnerClass = "show";
            return PartialView("_AnswerItem", answer);
        }

        public async Task<IActionResult> ProhibitAnswer(int? answerId)
        {
            if (answerId == null)
                return NoContent();

            var answer = await _context.Answers
                .Include(a => a.User)
                //.ThenInclude(u => u.Likes)
                .Include(a => a.Comment)
                .FirstOrDefaultAsync(a => a.Id == answerId);

            if (answer == null)
                return NoContent();

            answer.IsModerated = false;

            _context.Entry<Answer>(answer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            ViewBag.showAccordionInnerClass = "show";
            return PartialView("_AnswerItem", answer);
        }

        public async Task<IActionResult> DeleteAnswer(int? answerId)
        {
            if (answerId == null)
                return NoContent();

            var answer = await _context.Answers
                .FirstOrDefaultAsync(a => a.Id == answerId);

            if (answer == null)
                return NoContent();

            _context.Entry<Answer>(answer).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            ViewBag.message = "Answer was deleted";

            return PartialView("_DeletedAlert");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifiedAnswer(int id, Answer answer)
        {
            if (id != answer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Answers.Update(answer);
                await _context.SaveChangesAsync();
                return PartialView("_ModifiedModal");
            }

            return NoContent();
        }
    }
}
