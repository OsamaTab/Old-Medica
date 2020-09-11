using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Organizer.Controllers
{
    public class AccountController : BassController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly OrgDbContext _context;
        private readonly IProfileService _profileService;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , IHostingEnvironment hostingEnvironment,OrgDbContext context,IProfileService profileService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var model = await _profileService.GetProfile(user);

            ViewData["Specialties"] = new SelectList(_context.Specialties, "Id", "Name");
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                var result = await _profileService.EditProfile(user, model);
               
                if(result.Succeeded)
                {
                    return RedirectToAction("profile", "Account");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            ViewData["Specialties"] = new SelectList(_context.Specialties, "Id", "Name");
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName");

            return View(model);
        }

      

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.SignUp = true;
            return View("/Views/Account/Auth.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AuthViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = new ApplicationUser { UserName = userModel.UserName, Email = userModel.SignUpEmail };

            var result = await _userManager.CreateAsync(user, userModel.SignUpPassword);
            if (result.Succeeded)
            {
                user.PhotoPath = "Blank.png";
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return RedirectToAction("Register", "Account", new { userModel = userModel });

        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.SignUp = false;
            return View("/Views/Account/Auth.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userModel.LogInEmail);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, userModel.LogInPassword, userModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return RedirectToAction("Login", "Account", new {userModel=userModel });

        }
    }
}
