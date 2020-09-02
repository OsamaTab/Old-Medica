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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TimeLine> TimeLines { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Request>().HasData(
               new Request() { Id = 1, Status = "approved" },
               new Request() { Id = 2, Status = "pending" },
               new Request() { Id = 3, Status = "rejected" }
               );


        }
    }
}
