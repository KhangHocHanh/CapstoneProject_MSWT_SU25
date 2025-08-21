using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IWorkGroupMemberRepository : IGenericRepository<WorkGroupMember>
    {
        Task<WorkGroupMember> GetByIdAsync(string id);
        Task AddAsync(WorkGroupMember workGroupMember);
        Task DeleteAsync(string id);
        Task<IEnumerable<WorkGroupMember>> GetAllAsync();
        Task UpdateAsync(WorkGroupMember workGroupMember);
        Task<IEnumerable<WorkGroupMember>> GetByWorkGroupIdAsync(string workGroupId);
    }
}
