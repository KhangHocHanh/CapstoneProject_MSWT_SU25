using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

namespace MSWT_API.Controllers
{
    [Route("api/restrooms")]
    [ApiController]
    public class RestroomController : Controller
    {
        private readonly IRestroomService _restroomService;

        public RestroomController(IRestroomService restroomService)
        {
            _restroomService = restroomService;
        }

        #region CRUD Category

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restroom>>> GetAll()
        {
            return Ok(await _restroomService.GetAllRestrooms());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restroom>> GetById(string id)
        {
            var restroom = await _restroomService.GetRestroomById(id);
            if (restroom == null)
                return NotFound();
            return Ok(restroom);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _restroomService.DeleteRestroom(id);
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
