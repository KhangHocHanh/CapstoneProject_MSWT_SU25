using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IScheduleRepository
    {
        #region CRUD Category
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule> GetByIdAsync(string id);
        Task AddAsync(Schedule schedule);
        Task DeleteAsync(string id);
        Task UpdateAsync(Schedule schedule);
        #endregion
    }
}
