using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_API.Controllers
{
        [Route("api/workerGroup")]
        [ApiController]
        public class WorkerGroupController : ControllerBase
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
        [HttpPost]
        public async Task<IActionResult> CreateWorkerGroup([FromBody] CreateWorkerGroupRequest request)
        {
            try
            {
                var result = await _workerGroupService.CreateWorkerGroupAsync(request);
                return Ok(new { Success = true, Data = result, Message = "Worker group created successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Internal server error" });
            }
        }

        [HttpGet("get-by-{id}")]
        public async Task<IActionResult> GetWorkerGroupById(string id)
        {
            var result = await _workerGroupService.GetWorkerGroupByIdAsync(id);
            if (result == null)
                return NotFound(new { Success = false, Message = "Worker group not found" });

            return Ok(new { Success = true, Data = result });
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllWorkerGroups()
        {
            var result = await _workerGroupService.GetAllWorkerGroupsAsync();
            return Ok(new { Success = true, Data = result });
        }

        [HttpPut("get-by-{id}")]
        public async Task<IActionResult> UpdateWorkerGroup(string id, [FromBody] UpdateWorkerGroupRequest request)
        {
            var result = await _workerGroupService.UpdateWorkerGroupAsync(id, request);
            if (result == null)
                return NotFound(new { Success = false, Message = "Worker group not found" });

            return Ok(new { Success = true, Data = result, Message = "Worker group updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerGroup(string id)
        {
            var result = await _workerGroupService.DeleteWorkerGroupAsync(id);
            return Ok(new { Success = true, Message = "Worker group deleted successfully" });
        }

        [HttpGet("available-users")]
        public async Task<IActionResult> GetAvailableUsers()
        {
            var result = await _workerGroupService.GetAvailableUsersAsync();
            return Ok(new { Success = true, Data = result });
        }

        [HttpPost("{groupId}/members")]
        public async Task<IActionResult> AddMembersToGroup(string groupId, [FromBody] AddMembersToGroupRequest request)
        {
            try
            {
                var result = await _workerGroupService.AddMembersToGroupAsync(groupId, request);
                if (!result)
                    return NotFound(new { Success = false, Message = "Worker group not found" });

                return Ok(new { Success = true, Message = "Members added successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{groupId}/members/{userId}")]
        public async Task<IActionResult> RemoveMemberFromGroup(string groupId, string userId)
        {
            var result = await _workerGroupService.RemoveMemberFromGroupAsync(groupId, userId);
            if (!result)
                return NotFound(new { Success = false, Message = "Member not found in group" });

            return Ok(new { Success = true, Message = "Member removed successfully" });
        }
        [HttpPut("{groupId}")]
        public async Task<IActionResult> UpdateWorkerGroupWithMembers(string groupId, [FromBody] UpdateWorkerGroupWithMembersRequest request)
        {
            try
            {
                var result = await _workerGroupService.UpdateWorkerGroupWithMembersAsync(groupId, request);
                if (result == null)
                    return NotFound(new { Success = false, Message = "Worker group not found" });

                return Ok(new
                {
                    Success = true,
                    Data = result,
                    Message = "Worker group and members updated successfully"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

    }
}
