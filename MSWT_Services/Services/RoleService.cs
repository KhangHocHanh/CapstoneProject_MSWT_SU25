using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;

namespace MSWT_Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task AddRole(Role role)
        {
            await _roleRepository.AddAsync(role);
        }

        public async Task DeleteRole(string id)
        {
            await _roleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
           return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task UpdateRole(Role role)
        {
            await _roleRepository.UpdateAsync(role);
        }
    }
}
