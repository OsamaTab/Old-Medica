using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Organizer.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        
        private readonly IAccountService _accountService;
        private readonly OrgDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(IAccountService accountService, OrgDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _accountService.GetUsers();

            return View( model);
        }
       

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");

            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            var model = new UserViewModel
            {
                UserId = user.Id,
                UserName = user.Email,

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string userId, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.Edit(userId, model);
                return RedirectToAction("index", "account");
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(!await _userManager.IsInRoleAsync(user,"Admin"))
            {
                await _accountService.Delete(id);
                return RedirectToAction("index", "account");
            }
            return RedirectToAction("index", "account");
        }
    }
}
