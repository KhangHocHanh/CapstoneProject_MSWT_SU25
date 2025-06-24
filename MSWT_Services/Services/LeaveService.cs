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
    public class LeaveService : ILeaveService
    {
        private readonly ILeafRepository _leafRepository;
        public LeaveService(ILeafRepository leafRepository)
        {
            _leafRepository = leafRepository;
        }
        public async Task AddLeaf(Leaf Leaf)
        {
            await _leafRepository.AddAsync(Leaf);
        }

        public async Task DeleteLeaf(string id)
        {
            await _leafRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Leaf>> GetAllLeafs()
        {
            return await _leafRepository.GetAllAsync();
        }

        public async Task<Leaf> GetLeafById(string id)
        {
            return await _leafRepository.GetByIdAsync(id);
        }

        public async Task UpdateLeaf(Leaf Leaf)
        {
            await _leafRepository.UpdateAsync(Leaf);
        }
    }
}
