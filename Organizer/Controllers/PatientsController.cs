using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Model;
using Microsoft.AspNetCore.Identity;
using BusinessLogic.IServices;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace Organizer.Controllers
{
    [Authorize(Roles = "Doctors")]
    public class PatientsController : BassController
    {
        private readonly OrgDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientService _patientService;

        public PatientsController(OrgDbContext context, UserManager<ApplicationUser> userManager,IPatientService patientService)
        {
            _context = context;
            _userManager = userManager;
            _patientService = patientService;
        }

        // GET: Patients
        
        public async Task<IActionResult> Index(string? search,int? page=1)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var patients = _patientService.GetFilterdPatients(search , user.Id);
            ViewBag.Page = page;

            return View(PagingList.Create(patients, 12, (int)page));
        }

        // GET: Patients/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patients=await _patientService.GetPatient(id);

            if (patients == null)
            {
                return NotFound();
            }

            return View(patients);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Age,PhotoPath,Debt,Payed")] Patients patients)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                await _patientService.Create(user, patients);
                return RedirectToAction(nameof(Index));
            }
            return View(patients);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patients = await _context.Patients.FindAsync(id);
            if (patients == null)
            {
                return NotFound();
            }
            return View(patients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Age,PhotoPath,Debt,Payed")] Patients patients)
        {
            if (id != patients.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _patientService.Edit(id, patients);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientsExists(patients.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patients);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _patientService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PatientsExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
