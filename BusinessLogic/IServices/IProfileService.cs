using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IProfileService
    {
        public Task<ProfileViewModel> GetProfile(ApplicationUser user);
        public Task<IdentityResult> EditProfile(ApplicationUser user,ProfileViewModel model);

    }
}
