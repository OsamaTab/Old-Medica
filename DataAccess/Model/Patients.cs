using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public class Patients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public string PhotoPath { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public ApplicationUser Doctor { get; set; }
        public int Debt { get; set; }
        public int Payed { get; set; }
    }
}
