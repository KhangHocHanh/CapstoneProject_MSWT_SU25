using AutoMapper;
using MSWT_BussinessObject.Model;
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
    public class RestroomService : IRestroomService
    {
        private readonly IRestroomRepository _restroomRepository;
        private readonly IMapper _mapper;
        public RestroomService(IRestroomRepository restroomRepository, IMapper mapper)
        {
            _restroomRepository = restroomRepository;
            _mapper = mapper;
        }

        public async Task AddRestroom(Restroom restroom)
        {
            await _restroomRepository.AddAsync(restroom);
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

        public async Task UpdateRestroom(Restroom restroom)
        {
            await _restroomRepository.UpdateAsync(restroom);
        }
    }
}
