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
        public async Task<Patients> GetPatient(int? id)
        {
            var patients = await _context.Patients
                .Include(p => p.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            return patients;
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

        public async Task Create(ApplicationUser user, Patients patients)
        {
            patients.DoctorId = user.Id;
            _context.Add(patients);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Patients patients)
        {
            _context.Update(patients);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var patients = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patients);
            await _context.SaveChangesAsync();
        }

    }
}
