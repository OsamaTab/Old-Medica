using DataAccess.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data
{
    public class OrgDbContext : IdentityDbContext <ApplicationUser>
    {
        public OrgDbContext(DbContextOptions<OrgDbContext> options)
         : base(options)
        {
        }
    }
}
