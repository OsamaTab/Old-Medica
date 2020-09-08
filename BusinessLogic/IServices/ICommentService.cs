
using DataAccess.Model;
using DataAccess.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface ICommentService
    {
        public Task<List<Comments>> GetComments(string? id);
        public List<Comments> GetSubComments(string? id);
        public Task CreatComment(string? doctoId, string? userId, DoctorViewModel comment);
        public Task CreatSubComment();
        public Task Delete(string id);


    }
}
