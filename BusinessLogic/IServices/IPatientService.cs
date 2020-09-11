using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IPatientService
    {
        public Task<Patients> GetPatient(int? id);
        public List<Patients> GetFilterdPatients(string? search ,string id);
        public Task Create(ApplicationUser user,Patients patients);
        public Task Edit(int id,Patients patients);
        public Task Delete(int id);


    }
}
