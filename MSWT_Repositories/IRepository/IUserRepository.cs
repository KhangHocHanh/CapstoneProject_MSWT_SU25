using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        #region CRUD User
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task DeleteAsync(string id);
        Task UpdateAsync(User user);
        #endregion

        Task SoftDeleteAsync(string id);
        Task RecoverAsync(string id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetUsersByRoleAsync(string roleName);
    }
}
