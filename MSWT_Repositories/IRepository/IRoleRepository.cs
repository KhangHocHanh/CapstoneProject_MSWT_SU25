using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        #region CRUD Category
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> GetByIdAsync(string id);
        Task AddAsync(Role role);
        Task DeleteAsync(string id);
        Task UpdateAsync(Role role);
        #endregion


        Task<bool> IsRoleUsedAsync(string id);
        Task<Role> GetIdByNameAsync(string name);
        Task<string> GetNameByIdAsync(string id);
    }
}
