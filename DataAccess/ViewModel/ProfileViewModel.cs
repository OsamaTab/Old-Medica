
using DataAccess.Model;
using Microsoft.AspNetCore.Http;


namespace DataAccess.ViewModel
{
    public class ProfileViewModel : ApplicationUser
    {
        public IFormFile Photo { get; set; }
    }
}
