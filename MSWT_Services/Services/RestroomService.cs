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
    public class RestroomService : IRestroomService
    {
        private readonly IRestroomRepository _restroomRepository;
        public RestroomService(IRestroomRepository restroomRepository)
        {
            _restroomRepository = restroomRepository;
        }

        public async Task AddRestroom(Restroom restroom)
        {
            await _restroomRepository.AddAsync(restroom);
        }

        public async Task DeleteRestroom(string id)
        {
            await _restroomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Restroom>> GetAllRestrooms()
        {
            return await _restroomRepository.GetAllAsync();
        }

        public async Task<Restroom> GetRestroomById(string id)
        {
            return await _restroomRepository.GetByIdAsync(id);
        }

        public async Task UpdateRestroom(Restroom restroom)
        {
            await _restroomRepository.UpdateAsync(restroom);
        }
    }
}
