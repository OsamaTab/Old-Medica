using BusinessLogic.IServices;
using DataAccess.Data;
using DataAccess.Migrations;
using DataAccess.Model;
using DataAccess.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class DBComment : ICommentService
    {
        private readonly OrgDbContext _context;
        public DBComment(OrgDbContext context)
        {
            _context = context;
        }
        public async Task CreatComment(string? doctoId, string? userId, DoctorViewModel comment)
        {
            Comments c = new Comments() { 
                Doctor = doctoId,
                UserId = userId,
                Comment = comment.Comment.Comment,
                CreatedTime = DateTime.Now
            };
            _context.Add(c);
            await _context.SaveChangesAsync();
                
        }

        public Task CreatSubComment()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Comments>> GetComments(string id)
        {
            var co =_context.Comments.Include(x => x.user).Where(x => x.Doctor == id);
            return co.ToList();
        }

        public List<Comments> GetSubComments(string id)
        {
            throw new NotImplementedException();
        }
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
