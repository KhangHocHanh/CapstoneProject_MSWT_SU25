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
    }
}
