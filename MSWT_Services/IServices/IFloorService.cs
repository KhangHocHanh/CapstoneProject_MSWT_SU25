using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IFloorService
    {
        #region CRUD Category
        Task<IEnumerable<Floor>> GetAllFloors();
        Task<Floor> GetFloorById(string id);
        Task AddFloor(Floor floor);
        Task UpdateFloor(Floor floor);
        Task DeleteFloor(string id);
        #endregion
    }
}
