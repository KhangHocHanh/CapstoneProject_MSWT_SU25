using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IWorkerGroupRepository : IGenericRepository<WorkerGroup>
    {
        Task<WorkerGroup> GetByIdAsync(string id);
        Task AddAsync(WorkerGroup workerGroup);
        Task DeleteAsync(string id);
        Task<IEnumerable<WorkerGroup>> GetAllAsync();
        Task UpdateAsync(WorkerGroup workerGroup);
        Task<WorkerGroup> GetByIdWithMembersAsync(string id);
        Task<IEnumerable<WorkerGroup>> GetAllWithMembersAsync();
    }
}
