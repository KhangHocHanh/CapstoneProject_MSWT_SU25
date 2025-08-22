using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IGroupAssignmentRepository : IGenericRepository<GroupAssignment>
    {
        Task<IEnumerable<GroupAssignment>> GetAllAsync();
        Task AddAsync(GroupAssignment groupAssignment);
        Task<GroupAssignment> GetByIdAsync(string id);
        Task DeleteAsync(string id);
        Task UpdateAsync(GroupAssignment groupAssignment);
        Task<IEnumerable<GroupAssignment>> GetByAssignmentGroupIdAsync(string workGroupId);
    }
}
