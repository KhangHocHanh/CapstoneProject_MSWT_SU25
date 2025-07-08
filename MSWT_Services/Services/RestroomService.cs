using AutoMapper;
using CustomEnum = MSWT_BussinessObject.Enum;
using Azure.Core;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class RestroomService : IRestroomService
    {
        private readonly IRestroomRepository _restroomRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public RestroomService(IRestroomRepository restroomRepository,IAreaRepository areaRepository ,IMapper mapper)
        {
            _restroomRepository = restroomRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<RestroomResponseDTO> AddRestroom(RestroomRequestDTO request)
        {
            try
            {
                // Get the area by ID
                var area = await _areaRepository.GetByIdAsync(request.AreaId);
                if (area == null)
                {
                    throw new Exception("Area does not exist.");
                }

                var restroom = _mapper.Map<Restroom>(request);
                restroom.RestroomId = Guid.NewGuid().ToString();

                await _restroomRepository.AddAsync(restroom);
                return _mapper.Map<RestroomResponseDTO>(restroom);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to add restroom: {e.Message}");
            }
        }

        public async Task DeleteRestroom(string id)
        {
            await _restroomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RestroomResponseDTO>> GetAllRestrooms()
        {
            var restrooms = await _restroomRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RestroomResponseDTO>>(restrooms);
        }


        public async Task<RestroomResponseDTO?> GetRestroomById(string id)
        {
            var restroom = await _restroomRepository.GetByIdAsync(id);
            return _mapper.Map<RestroomResponseDTO?>(restroom);
        }

        public async Task<RestroomResponseDTO> UpdateRestroom(string restroomId, RestroomRequestDTO request)
        {
            try
            {
                var existingRestroom = await _restroomRepository.GetByIdAsync(restroomId);
                if (existingRestroom == null)
                {
                    throw new Exception("Restroom not found.");
                }

                var area = await _areaRepository.GetByIdAsync(request.AreaId);
                if (area == null)
                {
                    throw new Exception("Area does not exist.");
                }

                _mapper.Map(request, existingRestroom);
                await _restroomRepository.UpdateAsync(existingRestroom);

                return _mapper.Map<RestroomResponseDTO>(existingRestroom);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to update restroom: {e.Message}");
            }
        }

    }
}
