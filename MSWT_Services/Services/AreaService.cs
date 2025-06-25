using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
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
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public AreaService(IAreaRepository areaRepository, IMapper mapper)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<AreaResponseDTO> CreateAreaAsync(AreaRequestDTO request)
        {
            var area = _mapper.Map<Area>(request);
            area.AreaId = Guid.NewGuid().ToString(); // Generate UID

            await _areaRepository.AddAsync(area);
            return _mapper.Map<AreaResponseDTO>(area);
        }


        public async Task DeleteArea(string id)
        {
            await _areaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AreaResponseDTO>> GetAllAreasAsync()
        {
            var areas = await _areaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AreaResponseDTO>>(areas);
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
