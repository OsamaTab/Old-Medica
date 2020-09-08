using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class DBPatient : IPatientService
    {
        private readonly OrgDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DBPatient(OrgDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context=context;
            _userManager = userManager;
        }
        public List<Patients> GetFilterdPatients(string search,string id)
        {
            var patient = from m in _context.Patients.Include(i => i.Doctor).Where(x=>x.DoctorId == id)
                       select m;
            if (!String.IsNullOrEmpty(search))
            {
                patient = patient.Where(s => s.Name.Contains(search));
            }

            return patient.ToList();
        }
    }
}
