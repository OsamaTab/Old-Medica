using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; }
        public DateTime RequestDate { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public Request Request { get; set; }

    }
}
