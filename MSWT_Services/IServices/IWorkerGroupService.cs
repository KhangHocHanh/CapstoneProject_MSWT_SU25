using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IWorkerGroupService
    {
        Task<WorkerGroupResponse> GetWorkerGroupById(string id);
        Task<IEnumerable<WorkerGroupResponse>> GetAllWorkerGroups();
    }
}
