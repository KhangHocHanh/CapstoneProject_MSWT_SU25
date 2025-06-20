using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IShiftRepository
    {
        #region CRUD Category
        Task<IEnumerable<Shift>> GetAllAsync();
        Task<Shift> GetByIdAsync(string id);
        Task AddAsync(Shift shift);
        Task DeleteAsync(string id);
        Task UpdateAsync(Shift shift);
        #endregion
    }
}
