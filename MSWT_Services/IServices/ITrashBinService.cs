using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface ITrashBinService
    {
        #region CRUD Category
        Task<IEnumerable<TrashbinWithAreaNameDTO>> GetAllTrashBins();
        Task<TrashBin> GetTrashBinById(string id);
        Task AddTrashBin(TrashBin TrashBin);
        Task UpdateTrashBin(TrashBin TrashBin);
        Task DeleteTrashBin(string id);
        #endregion
    }
}
