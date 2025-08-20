using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;

        public BuildingService(IBuildingRepository buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task<BuildingResponseDTO> CreateBuildingAsync(BuildingRequestDTO request)
        {
            var building = _mapper.Map<Building>(request);
            building.BuildingId = Guid.NewGuid().ToString();

            await _buildingRepository.AddAsync(building);

            return _mapper.Map<BuildingResponseDTO>(building);
        }


        public async Task DeleteBuilding(string id)
        {
            await _buildingRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<BuildingResponseDTO>> GetAllBuildings()
        {
            var buildings = await _buildingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BuildingResponseDTO>>(buildings);
        }

        public async Task<BuildingResponseDTO> GetBuildingById(string id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null) return null;

            return _mapper.Map<BuildingResponseDTO>(building);
        }

        public async Task<bool> UpdateBuilding(string id, BuildingRequestDTO request)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            _mapper.Map(request, building);

            await _buildingRepository.UpdateAsync(building);
            return true;
        }
    }
}
