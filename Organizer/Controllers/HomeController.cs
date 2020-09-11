using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;

namespace Organizer.Controllers
{
    public class HomeController : BassController
    {
        private readonly IAccountService _accountService;
        private readonly ICommentService _commentService;
        private readonly OrgDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(IAccountService accountService, OrgDbContext context, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _context = context;
            _commentService=commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? search, int? specialty, int? city,int? page=1)
        {
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            ViewBag.Page = page;
            var docters = await _accountService.GetFilterdDoctors(search, specialty, city);
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                return View("/Views/Home/Main.cshtml",PagingList.Create(docters, 12, (int)page));
            }
            return View(PagingList.Create(docters, 12, (int)page));
        }
        public async Task<IActionResult> Detile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acount =await _accountService.GetDoctor(id);
            var comments =await _commentService.GetComments(id);
            var doctor = new DoctorViewModel()
            {
                Doctor = acount,
                Comments=comments
            };

            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id,DoctorViewModel co)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                await _commentService.CreatComment(id, user.Id, co);
                return RedirectToAction("Detile", "Home", new { id = id });
            }
            return RedirectToAction("Detile", "Home", new { id = id });

        }
    }
}
