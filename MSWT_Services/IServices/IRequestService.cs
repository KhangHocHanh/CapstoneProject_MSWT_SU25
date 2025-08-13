using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IRequestService
    {
        #region CRUD Request
        Task<IEnumerable<Request>> GetAllRequests();
        Task<Request> GetRequestById(string id);
        Task AddRequest(Request request);
        Task UpdateRequest(Request request);
        Task DeleteRequest(string id);
        #endregion
        Task SoftDeleteRequest(string id);
        Task<IEnumerable<RestroomResponseDTO>> GetAllRequestsWithWorkerName();
    }
}
