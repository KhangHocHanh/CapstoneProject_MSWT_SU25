using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #region CRUD Category
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            var roles = await _roleService.GetAllRoles();
            var activeRoles = roles
                .Where(r => r.Status == RoleStatus.DangHoatDong.ToDisplayString())  // "Đang hoạt động"
                .ToList();

            return Ok(activeRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(string id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
                return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Create([FromBody] RoleCreateDto dto)
        {
            var newRole = new Role
            {
                RoleId = "RL" + DateTime.UtcNow.Ticks,
                RoleName = dto.RoleName,
                Description = dto.Description,
                Status = RoleStatus.DangHoatDong.ToDisplayString()  // "Đang hoạt động"
            };

            await _roleService.AddRole(newRole);

            return CreatedAtAction(nameof(GetById),
                new { id = newRole.RoleId }, newRole);
        }
        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
                return NotFound(new { message = "Role không tồn tại" });

            // Đảo trạng thái
            if (role.Status == RoleStatus.DangHoatDong.ToDisplayString())
                role.Status = RoleStatus.NgungHoatDong.ToDisplayString();
            else
                role.Status = RoleStatus.DangHoatDong.ToDisplayString();

            await _roleService.UpdateRole(role);

            return Ok(new
            {
                message = "Cập nhật trạng thái thành công",
                newStatus = role.Status
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _roleService.DeleteRole(id);
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
