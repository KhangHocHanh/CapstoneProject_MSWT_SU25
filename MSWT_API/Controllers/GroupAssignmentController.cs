using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace MSWT_API.Controllers
{
    [Route("api/groupAssignment")]
    [ApiController]
    public class GroupAssignmentController : Controller
    {
        private readonly IGroupAssignmentService _groupAssignmentService;

        public GroupAssignmentController(IGroupAssignmentService groupAssignmentService)
        {
            _groupAssignmentService = groupAssignmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupAssignmentResponse>>> GetAll()
        {
            return Ok(await _groupAssignmentService.GetAllGroupAssignments());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupAssignmentResponse>> GetById(string id)
        {
            var groupAssignment = await _groupAssignmentService.GetGroupAssignmentById(id);
            if (groupAssignment == null)
                return NotFound();
            return Ok(groupAssignment);
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _groupAssignmentService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupAssignmentRequest request)
        {
            var group = await _groupAssignmentService.CreateAsync(request.Name, request.Description, request.AssignmentIds);
            return Ok(group);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] GroupAssignmentRequest request)
        {
            var group = await _groupAssignmentService.UpdateAsync(id, request.Name, request.Description, request.AssignmentIds);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _groupAssignmentService.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok(new { message = "Deleted successfully" });
        }
    }

    public class GroupAssignmentRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<string> AssignmentIds { get; set; } = new();
    }
}
