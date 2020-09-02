using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public class ApplicationUser :IdentityUser
    {
        public string PhotoPath { get; set; }
        public int? SpecialtyId { get; set; }
        [ForeignKey("SpecialtyId")]
        public Specialty Specialty { get; set; }
        public int? CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
