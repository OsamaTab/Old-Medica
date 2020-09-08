using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.ViewModel
{
    public class DoctorViewModel
    {
        public ApplicationUser Doctor { get; set; }
        public Comments Comment { get; set; }
        public List<Comments> Comments { get; set; }
        public List<Comments> SubComments { get; set; }

    }
}
