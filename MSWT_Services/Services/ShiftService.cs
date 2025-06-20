using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        public ShiftService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }
        public async Task AddShift(Shift shift)
        {
            await _shiftRepository.AddAsync(shift);
        }

        public async Task DeleteShift(string id)
        {
            await _shiftRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Shift>> GetAllShifts()
        {
            return await _shiftRepository.GetAllAsync();
        }

        public async Task<Shift> GetShiftById(string id)
        {
            return await _shiftRepository.GetByIdAsync(id);
        }

        public async Task UpdateShift(Shift shift)
        {
            await _shiftRepository.UpdateAsync(shift);
        }
    }
}
