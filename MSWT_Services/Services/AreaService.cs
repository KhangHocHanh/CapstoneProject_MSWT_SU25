using AutoMapper;
using CustomEnum = MSWT_BussinessObject.Enum;
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
using Azure.Core;

namespace MSWT_Services.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;
        public AreaService(IAreaRepository areaRepository, IMapper mapper, IBuildingRepository buildingRepository)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
            _buildingRepository = buildingRepository;
        }

        public async Task<AreaResponseDTO> CreateAreaAsync(AreaRequestDTO request)
        {
            var area = _mapper.Map<Area>(request);
            area.AreaId = Guid.NewGuid().ToString(); // Generate UID

            var building = await _buildingRepository.GetByIdAsync(area.BuildingId);
            if (building == null)
                throw new Exception("Building not found.");

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


        public async Task<AreaResponseDTO> GetAreaById(string id)
        {
            var area = await _areaRepository.GetByIdAsync(id);
            return _mapper.Map<AreaResponseDTO>(area);
        }

        public async Task UpdateArea(string areaId, AreaUpdateRequestDTO requestDto)
        {
            var existingArea = await _areaRepository.GetByIdAsync(areaId);
            if (existingArea == null)
                throw new Exception("Area not found");

            // Map updated fields from DTO into the existing entity
            _mapper.Map(requestDto, existingArea);
            await _areaRepository.UpdateAsync(existingArea);
        }

        public async Task<bool> AddBuildingToArea(string id, string buildingId)
        {
            try
            {
                var area = await _areaRepository.GetByIdAsync(id);
                if (area == null)
                    throw new Exception("Area not found.");

                var building = await _buildingRepository.GetByIdAsync(buildingId);
                if (building == null)
                    throw new Exception("Building not found.");

                area.Building = building;

                await _areaRepository.UpdateAsync(area);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating area detail : {e.Message}", e);
            }
        }
    }
}
