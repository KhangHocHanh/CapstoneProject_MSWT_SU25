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
        private readonly IFloorRepository _floorRepository;
        private readonly IMapper _mapper;
        public AreaService(IAreaRepository areaRepository, IMapper mapper, IFloorRepository floorRepository)
        {
            _areaRepository = areaRepository;
            _mapper = mapper;
            _floorRepository = floorRepository;
        }

        public async Task<AreaResponseDTO> CreateAreaAsync(AreaRequestDTO request)
        {
            var area = _mapper.Map<Area>(request);
            area.AreaId = Guid.NewGuid().ToString(); // Generate UID
            area.IsAssigned = "Yes";
            
            var floor = await _floorRepository.GetByIdAsync(area.FloorId);
            if (floor == null)
                throw new Exception("Floor not found.");

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

        public async Task<bool> AddFloorToArea(string id, string floorId)
        {
            try
            {
                var area = await _areaRepository.GetByIdAsync(id);
                if (area == null)
                    throw new Exception("Area not found.");

                var floor = await _floorRepository.GetByIdAsync(floorId);
                if (floor == null)
                    throw new Exception("Floor not found.");

                area.Floor = floor;
                area.IsAssigned = "yes";
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
