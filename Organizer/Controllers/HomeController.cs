using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReflectionIT.Mvc.Paging;

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

        public async Task<IActionResult> Index(string? search, int? specialty, int? city,int? page=1)
        {
            //var model = await _accountService.GetDoctors();
            var docters = await _accountService.GetFilterdDoctors(search, specialty, city);
            
            ViewData["SpecialtyId"] = new SelectList(_context.Specialties, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            return View(PagingList.Create(docters, 12, (int)page));
        }
        public IActionResult Detile()
        {
            return View();
        }
    }
}
