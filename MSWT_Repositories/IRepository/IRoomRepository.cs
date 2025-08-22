using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IRoomRepository
    {
        #region CRUD Category
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetByIdAsync(string id);
        Task AddAsync(Room room);
        Task DeleteAsync(string id);
        Task UpdateAsync(Room restroom);
        #endregion
    }
}
