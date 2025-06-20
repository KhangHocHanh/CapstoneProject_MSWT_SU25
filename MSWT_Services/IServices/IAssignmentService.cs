using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IAssignmentService
    {
        Task AddAssigment(Assignment assignment);
        Task DeleteAssigment(string id);
        Task<IEnumerable<Assignment>> GetAllAssigments();
        Task<Assignment> GetAssigmentById(string id);
        Task UpdateAssigment(Assignment assignment);
    }
}
