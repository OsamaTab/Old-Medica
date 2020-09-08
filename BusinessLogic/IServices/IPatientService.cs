using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IPatientService
    {
        public List<Patients> GetFilterdPatients(string? search ,string id);

    }
}
