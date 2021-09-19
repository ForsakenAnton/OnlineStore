using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using OnlineStore.Models.IdentityModels.ViewModels;
using OnlineStore.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class MyCabinetController : Controller
    {
        private readonly OnlineStoreContext _context;
        private readonly UserManager<User> _userManager;

        public MyCabinetController(OnlineStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            ViewBag.pageId = 1;
            return View();
        }

        public async Task<IActionResult> PersonalInformation(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            ViewBag.pageId = 2;
            return View(user);
        }

        public async Task<IActionResult> EditProfile(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return NoContent();
            }

            User user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            ChangePersonalInfoViewModel viewModel = new ChangePersonalInfoViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Lastname = user.Lastname,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email
            };

            return PartialView("_EditProfile", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditedProfile(string id, ChangePersonalInfoViewModel model)
        {
            if(id != model.Id)
            {
                return BadRequest();
            }

            ViewBag.pageId = 2;

            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                //_context.Entry<User>(user).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

                user.UserName = model.UserName;
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Lastname = model.Lastname;
                user.DateOfBirth = model.DateOfBirth;
                user.Email = model.Email;

                await _userManager.UpdateAsync(user);             
                
                ViewBag.successChanges = "The changes have been saved";
                return View("PersonalInformation", user);
            }

            string failedChanges = "The changes did not saved\n\n";

            foreach (ModelError error in ViewData.ModelState.Values.SelectMany(v => v.Errors))
            {
                failedChanges += " - " + error.ErrorMessage + "\n";
            }
            failedChanges = failedChanges.TrimEnd('\n');
            ViewBag.failedChanges = failedChanges.Split("\n");

            return View("PersonalInformation", user);
        }

        public IActionResult MyOrders()
        {
            ViewBag.pageId = 3;
            return View();
        }

        public IActionResult ViewedProducts()
        {
            ViewBag.pageId = 4;
            return View();
        }

        public async Task<IActionResult> FavoriteProducts(string userId)
        {
            if(String.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(u => u.FavoriteProducts)
                    .ThenInclude(fp => fp.Product)
                        .ThenInclude(P => P.Comments)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Id == userId);

            ViewBag.pageId = 5;
            return View(user);
        }
    }
}
