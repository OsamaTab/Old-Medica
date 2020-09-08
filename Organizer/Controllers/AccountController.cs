using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Model;
using DataAccess.ViewModel;
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


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostingEnvironment hostingEnvironment,OrgDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new ProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PhotoPath = user.PhotoPath,
                SpecialtyId = user.SpecialtyId,
                CityId = user.CityId

            };
            ViewData["Specialties"] = new SelectList(_context.Specialties, "Id", "Name");
            ViewData["Cities"] = new SelectList(_context.Cities, "Id", "CityName");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniceName = model.PhotoPath;
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (model.Photo != null)
                {
                    if (model.PhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img/profile/", model.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    uniceName = FileName(model);
                }
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.PhotoPath = uniceName;
                user.SpecialtyId = model.SpecialtyId;
                user.CityId = model.CityId;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
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

        private string FileName(ProfileViewModel user)
        {
            string uniqueFileName = null;
            if (user.Photo != null)
            {
                string upladeFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/profile/");
                uniqueFileName = Guid.NewGuid().ToString() + '_' + user.Photo.FileName;
                string filePath = Path.Combine(upladeFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
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
            ViewBag.SignUp = true;
            return View("/Views/Account/Auth.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userModel.LogInEmail, userModel.LogInPassword, userModel.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return RedirectToAction("Login", "Account", new {userModel=userModel });

        }
    }
}
