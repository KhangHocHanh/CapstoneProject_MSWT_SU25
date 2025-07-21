using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.RequestDTO;
using MSWT_Services.IServices;
using MSWT_Services.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CloudinaryController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public CloudinaryController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var result = await _cloudinaryService.UploadFile(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
