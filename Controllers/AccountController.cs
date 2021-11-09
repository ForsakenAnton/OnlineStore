using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineStore.Models.IdentityModels;
using OnlineStore.Models.IdentityModels.ViewModels;
using OnlineStore.Services;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                IdentityResult createdResult = await _userManager.CreateAsync(user: user, password: model.Password);
                if (createdResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");

                    await _signInManager.SignInAsync(user: user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    await _emailService.SendAsync("from_address@example.com", model.Email, "Confirm your account",
                        $"For confirm registration <a href='{callbackUrl}'>follow the link</a>");

                    return View("ConfirmRegistration");
                }
                else
                {
                    foreach (IdentityError error in createdResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("ConfirmedAccount", "Account"/*, new {userId = userId }*/);
            else
                return View("Error");
        }

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult /*async Task<IActionResult>*/ ConfirmedAccount(/*string userId = null*/)
        {
            //if (userId == null)
            //    return View("Error");

            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //{
            //    return View("Error");
            //}

            return View(/*user*/);
        }



        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                //if(user == null)
                //{
                //    return NotFound();
                //}
                if (user != null)
                {
                    //проверяем, подтвержден ли email
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "You are not confirmed your Email");
                        return View(model);
                    }
                }

                string userName = user != null ? user.UserName : model.Email;
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(
                    userName,  // model.Email,
                    model.Password,
                    isPersistent: model.RememberMe, // Флаг, указывающий, должен ли файл cookie для входа сохраняться после закрытия браузера.
                    lockoutOnFailure: false); // Флаг, указывающий, следует ли блокировать учетную запись пользователя в случае сбоя входа.

                if (signInResult.Succeeded)
                {

                    //избегагаем перенаправления на нежелательные сайты
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login and (or) password");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //SignOutResult signOutResult =
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                if(user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                await _emailService.SendAsync("from_address@example.com",
                    model.Email,
                    "Reset Password",
                    $"To reset password <a href='{callbackUrl}'>follow the link</a>");

                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }

            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
