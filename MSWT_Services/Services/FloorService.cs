//using AutoMapper;
//using MSWT_BussinessObject.Model;
//using MSWT_BussinessObject.RequestDTO;
//using MSWT_BussinessObject.ResponseDTO;
//using MSWT_Repositories.IRepository;
//using MSWT_Services.IServices;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MSWT_Services.Services
//{
//    public class FloorService : IFloorService
//    {
//        private readonly IFloorRepository _floorRepository;
//        private readonly IMapper _mapper;

//        public FloorService(IFloorRepository floorRepository, IMapper mapper)
//        {
//            _floorRepository = floorRepository;
//            _mapper = mapper;
//        }

//        public async Task<FloorResponseDTO> CreateFloorAsync(FloorRequestDTO request)
//        {
//            var floor = new Floor
//            {
//                FloorId = Guid.NewGuid().ToString(),
//                NumberOfRestroom = request.NumberOfRestroom,
//                NumberOfBin = request.NumberOfBin,
//                Status = request.Status,
//                FloorNumber = request.FloorNumber
//            };

//            await _floorRepository.AddAsync(floor);

//            return _mapper.Map<FloorResponseDTO>(floor);
//        }


//        public async Task DeleteFloor(string id)
//        {
//            await _floorRepository.DeleteAsync(id);
//        }

//        public async Task<IEnumerable<FloorResponseDTO>> GetAllFloors()
//        {
//            var floors = await _floorRepository.GetAllAsync();
//            return _mapper.Map<IEnumerable<FloorResponseDTO>>(floors);
//        }

//        public async Task<FloorResponseDTO> GetFloorById(string id)
//        {
//            var floor = await _floorRepository.GetByIdAsync(id);
//            if (floor == null) return null;

//            return _mapper.Map<FloorResponseDTO>(floor);
//        }

//        public async Task<bool> UpdateFloor(string id, FloorRequestDTO request)
//        {
//                var floor = await _floorRepository.GetByIdAsync(id);
//                _mapper.Map(request, floor);

//                await _floorRepository.UpdateAsync(floor);
//                return true;
//        }
//    }
//}
