using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;

namespace MSWT_Repositories.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            _context.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role> GetByIdAsync(string id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }

        public async Task<Role> GetIdByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName.ToLower() == name.ToLower());
        }

        public async Task<string> GetNameByIdAsync(string id)
        {
            var roleName = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
            return roleName.RoleName;

        }

        public async Task<bool> IsRoleUsedAsync(string id)
        {
            return await _context.Users.AnyAsync(u => u.RoleId == id);
        }

        async Task<IEnumerable<Role>> IRoleRepository.GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}
