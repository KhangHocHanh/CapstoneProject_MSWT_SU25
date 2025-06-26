using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;

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
        public async Task AddLeave(Leaf leave)
        {
            leave.ApprovalStatus = ApprovalStatusEnum.ChuaDuyet.ToVietnamese();
            await _leafRepository.AddAsync(leave);
        }

        public async Task<Leaf?> ApproveLeave(string leaveId, string approverId)
        {
            var leave = await _leafRepository.GetByIdAsync(leaveId);
            if (leave == null) return null;

            leave.ApprovalStatus = ApprovalStatusEnum.DaDuyet.ToVietnamese();
            leave.ApprovedBy = approverId;

            await _leafRepository.UpdateAsync(leave);
            return leave;
        }

        public async Task<IEnumerable<Leaf>> GetLeaves(string userId, string role)
        {
            var allLeaves = await _leafRepository.GetAllAsync();
            if (role.ToLower() == "leader")
            {
                return allLeaves;
            }
            return allLeaves.Where(l => l.WorkerId == userId);
        }

        public async Task<Leaf?> GetLeaveById(string id)
        {
            return await _leafRepository.GetByIdAsync(id);
        }

        public async Task<bool> DeleteLeave(string id)
        {
            var leave = await _leafRepository.GetByIdAsync(id);
            if (leave == null) return false;

            await _leafRepository.DeleteAsync(id);
            return true;
        }
        public async Task<IEnumerable<Leaf>> GetLeavesByUser(string userId)
        {
            return await _leafRepository.GetLeavesByUserId(userId);
        }

    }
}
