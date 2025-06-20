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
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;
        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public async Task AddFloor(Floor floor)
        {
            await _floorRepository.AddAsync(floor);
        }

        public async Task DeleteFloor(string id)
        {
            await _floorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Floor>> GetAllFloors()
        {
            return await _floorRepository.GetAllAsync();
        }

        public async Task<Floor> GetFloorById(string id)
        {
            return await _floorRepository.GetByIdAsync(id);
        }

        public async Task UpdateFloor(Floor floor)
        {
            await _floorRepository.UpdateAsync(floor);
        }
    }
}
