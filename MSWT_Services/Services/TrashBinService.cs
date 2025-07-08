using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_Repositories;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class TrashBinService : ITrashBinService
    {
        private readonly ITrashBinRepository _TrashBinRepository;
        private readonly IMapper _mapper;
        public TrashBinService(ITrashBinRepository TrashBinRepository, IMapper mapper)
        {
            _TrashBinRepository = TrashBinRepository;
            _mapper = mapper;
        }

        public async Task AddTrashBin(TrashBin TrashBin)
        {
            await _TrashBinRepository.AddAsync(TrashBin);
        }

        public async Task DeleteTrashBin(string id)
        {
            await _TrashBinRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TrashbinWithAreaNameDTO>> GetAllTrashBins()
        {
            var trashbins = await _TrashBinRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TrashbinWithAreaNameDTO>>(trashbins);
        }

        public async Task<TrashBin> GetTrashBinById(string id)
        {
            return await _TrashBinRepository.GetByIdAsync(id);
        }

        public async Task UpdateTrashBin(TrashBin TrashBin)
        {
            await _TrashBinRepository.UpdateAsync(TrashBin);
        }
    }
}
