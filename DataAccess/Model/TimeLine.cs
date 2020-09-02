using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public class TimeLine
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedTime { get; set; }
        public int PatientsId { get; set; }
        [ForeignKey("PatientsId")]
        public Patients Patient { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public ApplicationUser Doctor { get; set; }
    }
}
