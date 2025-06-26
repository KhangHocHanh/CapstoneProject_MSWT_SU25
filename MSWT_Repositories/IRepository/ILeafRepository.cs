using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface ILeafRepository : IGenericRepository<Leaf>
    {
        #region CRUD Category
        Task<IEnumerable<Leaf>> GetAllAsync();
        Task<Leaf> GetByIdAsync(string id);
        Task AddAsync(Leaf Leaf);
        Task DeleteAsync(string id);
        Task UpdateAsync(Leaf Leaf);
        Task<IEnumerable<Leaf>> GetLeavesByUserId(string userId);

        #endregion
    }
}
