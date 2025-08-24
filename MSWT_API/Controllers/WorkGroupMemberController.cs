using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;

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
    }
}
