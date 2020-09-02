using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Organizer.Controllers
{
    public class HomeController : BassController
    {
        private readonly IAccountService _accountService;
        private readonly OrgDbContext _context;

        public HomeController(IAccountService accountService, OrgDbContext context)
        {
            _accountService = accountService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _accountService.GetDoctors();
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");

            return View(model);
        }
        public IActionResult Detile()
        {
            return View();
        }
    }
}
