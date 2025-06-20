using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface IRoleService
    {
        #region CRUD Category
        Task<IEnumerable<Role>> GetAllRoles();
        Task<Role> GetRoleById(string id);
        Task AddRole(Role role);
        Task UpdateRole(Role role);
        Task DeleteRole(string id);
        #endregion
    }
}
