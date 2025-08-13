using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;


namespace MSWT_Repositories.Repository
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Request request)
        {
            _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var request = _context.Requests.Find(id);
            if(request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Request> GetByIdAsync(string id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            return await _context.Requests
                .Include(r => r.Worker)
                .ToListAsync();
        }

        public async Task UpdateAsync(Request request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();

        }
    }
}
