using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IRequestService _requestService;

        public RequestController(IRoleService roleService,IRequestService requestService )
        {
            _roleService = roleService;
            _requestService = requestService;
        }


        #region CRUD Category

        // GET api/requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetAsync()
        {
            var requests = await _requestService.GetAllRequests();
            return Ok(requests);
        }

        // GET api/requests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetByIdAsync(string id)
        {
            var request = await _requestService.GetRequestById(id);
            return request is null ? NotFound() : Ok(request);
        }

        // DELETE api/requests/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _requestService.DeleteRequest(id);
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
