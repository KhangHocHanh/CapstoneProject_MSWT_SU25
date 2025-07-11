using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace MSWT_API.Controllers
{
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentController : Controller
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        #region CRUD Category
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentRequestDTO request)
        {
            var result = await _assignmentService.CreateAssignmentAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAll()
        {
            return Ok(await _assignmentService.GetAllAssigments());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetById(string id)
        {
            var assignment = await _assignmentService.GetAssignmentById(id);
            if (assignment == null)
                return NotFound();
            return Ok(assignment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _assignmentService.DeleteAssigment(id);
                return NoContent(); // 204 No Content if success
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // 400 Bad Request if error
            }
        }
        #endregion

    }
}
