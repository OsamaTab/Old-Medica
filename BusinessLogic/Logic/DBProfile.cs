using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class DBProfile : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly OrgDbContext _context;

        public DBProfile(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHostingEnvironment hostingEnvironment, OrgDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public async Task<ProfileViewModel> GetProfile(ApplicationUser user)
        {
            var model = new ProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PhotoPath = user.PhotoPath,
                SpecialtyId = user.SpecialtyId,
                CityId = user.CityId

            };
            return model;
        }

        public async Task<IdentityResult> EditProfile(ApplicationUser user,ProfileViewModel model)
        {
            string uniceName = model.PhotoPath;
            if (model.Photo != null)
            {
                if (model.PhotoPath != null)
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img/profile/", model.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                uniceName = FileName(model);
            }
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PhotoPath = uniceName;
            user.SpecialtyId = model.SpecialtyId;
            user.CityId = model.CityId;

            IdentityResult result = await _userManager.UpdateAsync(user);
            return result;
        }


        public string FileName(ProfileViewModel user)
        {
            string uniqueFileName = null;
            if (user.Photo != null)
            {
                string upladeFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img/profile/");
                uniqueFileName = Guid.NewGuid().ToString() + '_' + user.Photo.FileName;
                string filePath = Path.Combine(upladeFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
