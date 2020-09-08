using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Model
{
    public class Comments
    {
        public Comments()
        {
            SubComment = false;
            CommentId = null;
        }
        public int Id { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser user { get; set; }
        public string Doctor { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool SubComment { get; set; }
        public string CommentId { get; set; }
    }
}
