using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSWT_BussinessObject.Model;
using MSWT_Services.IServices;

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
            return Ok(await _roleService.GetAllRoles());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(string id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
                return NotFound();
            return Ok(role);
        }

        //[HttpPost]
        //public async Task<ActionResult> Create([FromBody] RoleDTO roleDto)
        //{
        //    var role = new Role
        //    {
        //        RoleName = roleDto.RoleName,
        //    };

        //    await _roleService.AddRole(role);
        //    return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, role);

        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(string id, [FromBody] RoleDTO roleDto)
        //{
        //    var existingRole = await _roleService.GetRoleById(id);
        //    if (existingRole == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update only allowed properties
        //    existingRole.RoleName = roleDto.RoleName;

        //    await _roleService.UpdateRole(existingRole);
        //    return NoContent();

        //}

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


        //[HttpGet("byname/{name}")]
        //public async Task<ActionResult<Role>> GetIdByName(string name)
        //{
        //    var role = await _roleService.GetIdRoleByName(name);
        //    if (role == null)
        //        return NotFound();
        //    return Ok(role);
        //}

    }
}
