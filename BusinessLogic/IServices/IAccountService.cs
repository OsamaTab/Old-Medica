using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.ViewModel;
using DataAccess.Model;

namespace BusinessLogic.IServices
{
    public interface IAccountService
    {
        public List<IdentityRole> GetRole();
        public Task<List<UserViewModel>> GetUsers();
        public Task<ApplicationUser> GetDoctor(string? id);

        public Task<List<ApplicationUser>> GetDoctors();

        public Task<List<ApplicationUser>> GetFilterdDoctors(string? search,int? specialty,int? city);

        public Task Edit(string userId, UserViewModel model);
        public Task Delete(string id);

    }
}
