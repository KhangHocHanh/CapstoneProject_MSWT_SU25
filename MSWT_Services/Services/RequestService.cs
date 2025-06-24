using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;

namespace MSWT_Services.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;

        public RequestService(IRequestRepository requestRepository, IUserRepository userRepository)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
        }
        public async Task AddRequest(Request request)
        {
            await _requestRepository.AddAsync(request);
        }

        public async Task DeleteRequest(string id)
        {
            await _requestRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Request>> GetAllRequests()
        {
            return await _requestRepository.GetAllAsync();
        }

        public async Task<Request> GetRequestById(string id)
        {
            return await _requestRepository.GetByIdAsync(id);
        }

        public async Task SoftDeleteRequest(string id)
        {
            await _requestRepository.DeleteAsync(id);
        }

        public async Task UpdateRequest(Request request)
        {
            await _requestRepository.UpdateAsync(request);
        }
    }
}
