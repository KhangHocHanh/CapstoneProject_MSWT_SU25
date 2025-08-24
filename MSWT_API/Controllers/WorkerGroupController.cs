using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace MSWT_API.Controllers
{
        [Route("api/workerGroup")]
        [ApiController]
        public class WorkerGroupController : Controller
        {
            private readonly IWorkerGroupService _workerGroupService;

        public WorkerGroupController(IWorkerGroupService workerGroupService)
        {
            _workerGroupService = workerGroupService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkerGroupResponse>>> GetAll()
        {
            var groups = await _workerGroupService.GetAllWorkerGroups();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkerGroupResponse>> GetById(string id)
        {
            var group = await _workerGroupService.GetWorkerGroupById(id);
            if (group == null)
                return NotFound($"WorkerGroup with Id {id} not found.");

            return Ok(group);
        }
    }
}
