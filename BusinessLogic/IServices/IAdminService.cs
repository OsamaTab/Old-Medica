using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IAdminService
    {
        public Task CreateCity();
        public Task CreateSpecialty();
        public Task CreateContactUs();

    }
}
