using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeafRepository _leafRepository;
        private readonly IUserRepository _userRepository;
        public LeaveService(ILeafRepository leafRepository, IUserRepository userRepository)
        {
            _leafRepository = leafRepository;
            _userRepository = userRepository;
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
        public async Task<List<Leaf>> GetApprovedLeavesInMonth(int year, int month)
        {
            var allLeaves = await _leafRepository.GetAllAsync();
            return allLeaves
                .Where(l => l.ApprovalStatus == ApprovalStatusEnum.DaDuyet.ToVietnamese() &&
                            l.ApprovalDate.HasValue &&
                            l.ApprovalDate.Value.Year == year &&
                            l.ApprovalDate.Value.Month == month)
                .ToList();
        }
        public async Task<IEnumerable<LeafDTO>> GetAllLeafsWithFullName()
        {
            var leaves = await _leafRepository.GetAllAsync();
            return leaves.Select(l => new LeafDTO
            {
                LeaveId = l.LeaveId,
                WorkerId = l.WorkerId,
                LeaveType = l.LeaveType,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                TotalDays = l.TotalDays,
                Reason = l.Reason,
                RequestDate = l.RequestDate,
                ApprovalStatus = l.ApprovalStatus,
                ApprovedBy = l.ApprovedBy,
                ApprovalDate = l.ApprovalDate,
                Note = l.Note,
                FullName = l.Worker.FullName // lấy từ entity User
            }).ToList();
        }

        public async Task UpdateUsersOnLeaveAsync()
        {
            var allLeaves = await _leafRepository.GetAllAsync();
            var today = DateOnly.FromDateTime(DateTime.Now);

            foreach (var leave in allLeaves)
            {
                var user = leave.Worker;
                if (user == null) continue;

                if (leave.ApprovalStatus == "Đã duyệt" &&
                    today >= leave.StartDate &&
                    today <= leave.EndDate)
                {
                    if (user.Status != "Nghỉ phép")
                    {
                        user.Status = "Nghỉ phép";
                        await _userRepository.UpdateAsync(user);
                    }
                }
                else if (today > leave.EndDate && user.Status == "Nghỉ phép")
                {
                    user.Status = "Hoạt động"; // Trạng thái cũ giả định là "Đang làm việc"
                    await _userRepository.UpdateAsync(user);
                }
            }
        }

    }
}
