using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface ITrashBinService
    {
        #region CRUD Category
        Task<IEnumerable<TrashBin>> GetAllTrashBins();
        Task<TrashBin> GetTrashBinById(string id);
        Task AddTrashBin(TrashBin TrashBin);
        Task UpdateTrashBin(TrashBin TrashBin);
        Task DeleteTrashBin(string id);
        #endregion
    }
}
