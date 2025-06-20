using MSWT_BussinessObject.Model;
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
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task AddArea(Area area)
        {
            await _areaRepository.AddAsync(area);
        }

        public async Task DeleteArea(string id)
        {
            await _areaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Area>> GetAllAreas()
        {
            return await _areaRepository.GetAllAsync();
        }

        public async Task<Area> GetAreaById(string id)
        {
            return await _areaRepository.GetByIdAsync(id);
        }

        public async Task UpdateArea(Area area)
        {
            await _areaRepository.UpdateAsync(area);
        }
    }
}
