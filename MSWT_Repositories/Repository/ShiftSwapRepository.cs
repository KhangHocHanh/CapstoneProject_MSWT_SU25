using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Repositories.Repository
{
    public class ShiftSwapRepository : IShiftSwapRepository
    {
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;
        private readonly IMapper _mapper;

        public ShiftSwapRepository(SmartTrashBinandCleaningStaffManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ShiftSwapRequest> CreateRequestAsync(ShiftSwapRequest request)
        {
            _context.ShiftSwapRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<List<ShiftSwapResponseDTO>> GetRequestsForUserAsync(string userId)
        {
            return await _context.ShiftSwapRequests
                .Where(r => r.RequesterId == userId || r.TargetUserId == userId)
                .OrderByDescending(r => r.RequestDate)
                .AsNoTracking()
                .ProjectTo<ShiftSwapResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ShiftSwapRequest?> GetByIdAsync(Guid id)
        {
            return await _context.ShiftSwapRequests.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
