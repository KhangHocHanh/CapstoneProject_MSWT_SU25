using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IShiftService
    {
        #region CRUD Category
        Task<IEnumerable<Shift>> GetAllShifts();
        Task<Shift> GetShiftById(string id);
        Task AddShift(Shift shift);
        Task UpdateShift(Shift shift);
        Task DeleteShift(string id);
        #endregion
    }
}
