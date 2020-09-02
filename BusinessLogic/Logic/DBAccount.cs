using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class DBAccount :IAccountService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OrgDbContext _context;


        public DBAccount(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, OrgDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }



        public async Task<List<UserViewModel>> GetUsers()
        {
            var model = new List<UserViewModel>();

            foreach (var user in _userManager.Users)
            {
                
                    var userRole = new UserViewModel
                    {
                        UserId = user.Id,
                        UserName = user.Email
                    };
                    foreach (var role in _roleManager.Roles)
                    {
                        if (await _userManager.IsInRoleAsync(user, role.Name))
                        {
                            userRole.RoleName = role.Name;
                            userRole.RoleId = role.Id;
                        }
                    }
                    model.Add(userRole);
                
            }
            return model.ToList();
        }
        public async Task<List<ApplicationUser>> GetDoctors()
        {
            var docters = await _userManager.GetUsersInRoleAsync("Doctors");
          
            return docters.ToList();
        }

        public List<IdentityRole> GetRole()
        {
            var role = _roleManager.Roles;
            return role.ToList();
        }
        public async Task Edit(string userId,UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            user.Email = model.UserName;
            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        public async Task<List<ApplicationUser>> GetFilterdDoctors(string? search, int? specialty, int? city)
        {
            var docters = await _userManager.GetUsersInRoleAsync("Doctors");
   
            if (specialty != null)
            {
                docters = docters.Where(s => s.SpecialtyId == specialty).ToList();
            }
            if (city != null)
            {
                docters = docters.Where(s => s.CityId == city).ToList();
            }
            if (!String.IsNullOrEmpty(search))
            {
                docters = docters.Where(s => s.UserName.Contains(search)).ToList();
            }

            return docters.ToList();
        }

        public async Task Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }

    }
}
