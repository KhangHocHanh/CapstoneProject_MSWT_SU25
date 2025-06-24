using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;

namespace MSWT_Services.Services
{
    public class TrashBinService : ITrashBinService
    {
        private readonly ITrashBinRepository _TrashBinRepository;
        public TrashBinService(ITrashBinRepository TrashBinRepository)
        {
            _TrashBinRepository = TrashBinRepository;
        }

        public async Task AddTrashBin(TrashBin TrashBin)
        {
            await _TrashBinRepository.AddAsync(TrashBin);
        }

        public async Task DeleteTrashBin(string id)
        {
            await _TrashBinRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TrashBin>> GetAllTrashBins()
        {
            return await _TrashBinRepository.GetAllAsync();
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
