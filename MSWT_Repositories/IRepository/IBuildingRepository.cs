using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IBuildingRepository : IGenericRepository<Building>
    {
        #region CRUD Category
        Task<IEnumerable<Building>> GetAllAsync();
        Task<Building> GetByIdAsync(string id);
        Task AddAsync(Building building);
        Task DeleteAsync(string id);
        Task UpdateAsync(Building building);
        #endregion
    }
}
