using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IAssignmentRepository : IGenericRepository<Assignment>
    {
        #region CRUD Category
        Task<IEnumerable<Assignment>> GetAllAsync();
        Task<Assignment> GetByIdAsync(string id);
        Task AddAsync(Assignment assignment);
        Task DeleteAsync(string id);
        Task UpdateAsync(string assignmentId, Assignment assignment);
        #endregion
    }
}
