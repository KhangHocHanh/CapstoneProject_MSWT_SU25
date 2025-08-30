using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class GroupAssignmentService : IGroupAssignmentService
    {
        private readonly IGroupAssignmentRepository _groupAssignmentRepository;
        private readonly IMapper _mapper;
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;

        public GroupAssignmentService(IGroupAssignmentRepository groupAssignmentRepository, IMapper mapper, SmartTrashBinandCleaningStaffManagementContext context)
        {
            _groupAssignmentRepository = groupAssignmentRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<GroupAssignment> CreateAsync(string name, string? description, List<string> assignmentIds)
        {
            var newGroup = new GroupAssignment
            {
                GroupAssignmentId = Guid.NewGuid().ToString(),
                AssignmentGroupName = name,
                Description = description,
                CreatedAt = DateTime.Now
            };

            // Gán assignments
            var assignments = await _context.Assignments
                .Where(a => assignmentIds.Contains(a.AssignmentId))
                .ToListAsync();

            foreach (var assignment in assignments)
            {
                assignment.GroupAssignmentId = newGroup.GroupAssignmentId;
            }

            await _groupAssignmentRepository.AddAsync(newGroup);
            await _groupAssignmentRepository.SaveChangesAsync();

            return newGroup;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var group = await _groupAssignmentRepository.GetByIdAsync(id);
            if (group == null) return false;

            await _groupAssignmentRepository.DeleteAsync(group);
            await _groupAssignmentRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<GroupAssignment>> GetAllAsync()
        {
            return await _groupAssignmentRepository.GetAll();
        }

        public async Task<IEnumerable<GroupAssignmentResponse>> GetAllGroupAssignments()
        {
            var groupAssignments = await _groupAssignmentRepository.GetAll();
            return _mapper.Map<IEnumerable<GroupAssignmentResponse>>(groupAssignments);
        }

        public async Task<GroupAssignment?> GetByIdAsync(string id)
        {
            return await _groupAssignmentRepository.GetByIdAsync(id);
        }

        public async Task<GroupAssignmentResponse> GetGroupAssignmentById(string id) 
        {
            var groupAssignment = await _groupAssignmentRepository.GetByIdAsync(id);
            if (groupAssignment == null) return null;

            return _mapper.Map<GroupAssignmentResponse>(groupAssignment);
        }

        public async Task<GroupAssignment?> UpdateAsync(string id, string name, string? description, List<string> assignmentIds)
        {
            var group = await _groupAssignmentRepository.GetByIdAsync(id);
            if (group == null) return null;

            group.AssignmentGroupName = name;
            group.Description = description;

            // Clear assignments cũ
            var oldAssignments = await _context.Assignments
                .Where(a => a.GroupAssignmentId == id)
                .ToListAsync();

            foreach (var a in oldAssignments)
                a.GroupAssignmentId = null;

            // Add assignments mới
            var newAssignments = await _context.Assignments
                .Where(a => assignmentIds.Contains(a.AssignmentId))
                .ToListAsync();

            foreach (var a in newAssignments)
                a.GroupAssignmentId = id;

            await _groupAssignmentRepository.UpdateAsync(group);
            await _groupAssignmentRepository.SaveChangesAsync();

            return group;
        }
    }
}
