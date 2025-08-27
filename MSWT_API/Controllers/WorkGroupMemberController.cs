using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_API.Controllers
{
    [Route("api/workGroupMember")]
    [ApiController]
    public class WorkGroupMemberController : Controller
    {
        private readonly IWorkGroupMemberService _workGroupMemberService;

        public WorkGroupMemberController(IWorkGroupMemberService workGroupMemberService)
        {
            _workGroupMemberService = workGroupMemberService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkerGroupWithMembers([FromBody] WorkGroupMemberRequestDTO request)
        {
            var group = await _workGroupMemberService.CreateWorkerGroupWithMembersAsync(request);
            return Ok(group);
        }

        [HttpGet("{workGroupId}/members-with-supervisor")]
        public async Task<IActionResult> GetSupervisorAndMembers(string workGroupId)
        {
            var result = await _workGroupMemberService.GetSupervisorAndMembersByWorkGroupIdAsync(workGroupId);
            return Ok(result);
        }

        [HttpGet("{workGroupId}/members")]
        public async Task<IActionResult> GetMembersByWorkGroup(string workGroupId)
        {
            var members = await _workGroupMemberService.GetMembersByWorkGroupIdAsync(workGroupId);
            return Ok(members);
        }

        [HttpGet("all-members")]
        public async Task<IActionResult> GetAllWorkGroupMembers()
        {
            var members = await _workGroupMemberService.GetAllWorkGroup();
            return Ok(members);
        }

        [HttpGet("member/{id}")]
        public async Task<IActionResult> GetWorkGroupMemberById(string id)
        {
            var member = await _workGroupMemberService.GetWorkGroupMemberById(id);
            if (member == null) return NotFound();
            return Ok(member);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(string id)
        {
            var result = await _workGroupMemberService.GetMemberByIdAsync(id);
            if (result == null)
                return NotFound(new { Success = false, Message = "Member not found" });

            return Ok(new { Success = true, Data = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var result = await _workGroupMemberService.GetAllMembersAsync();
            return Ok(new { Success = true, Data = result });
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetMembersByGroupId(string groupId)
        {
            var result = await _workGroupMemberService.GetMembersByGroupIdAsync(groupId);
            return Ok(new { Success = true, Data = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(string id, [FromBody] UpdateWorkGroupMemberRequest request)
        {
            var result = await _workGroupMemberService.UpdateMemberAsync(id, request);
            if (result == null)
                return NotFound(new { Success = false, Message = "Member not found" });

            return Ok(new { Success = true, Data = result, Message = "Member updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var result = await _workGroupMemberService.DeleteMemberAsync(id);
            return Ok(new { Success = true, Message = "Member deleted successfully" });
        }
    }
}
