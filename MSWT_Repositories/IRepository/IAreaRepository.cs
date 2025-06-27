using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IAreaRepository : IGenericRepository<Area>
    {
        #region CRUD Category
        Task<IEnumerable<Area>> GetAllAsync();
        Task<Area> GetByIdAsync(string id);
        Task AddAsync(Area area);
        Task DeleteAsync(string id);
        Task UpdateAsync(Area area);
        #endregion
    }
}
