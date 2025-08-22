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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IAreaRepository _areaRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository restroomRepository, IAreaRepository areaRepository, IMapper mapper)
        {
            _roomRepository = restroomRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<RoomResponseDTO> AddRoom(RoomRequestDTO request)
        {
            try
            {
                // Get the area by ID
                var area = await _areaRepository.GetByIdAsync(request.AreaId);
                if (area == null)
                {
                    throw new Exception("Area does not exist.");
                }

                var room = _mapper.Map<Room>(request);
                room.RoomId = Guid.NewGuid().ToString();

                await _roomRepository.AddAsync(room);
                return _mapper.Map<RoomResponseDTO>(room);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to add room: {e.Message}");
            }
        }

        public async Task DeleteRoom(string id)
        {
            await _roomRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoomResponseDTO>> GetAllRooms()
        {
            var restrooms = await _roomRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoomResponseDTO>>(restrooms);
        }


        public async Task<RoomResponseDTO?> GetRoomById(string id)
        {
            var restroom = await _roomRepository.GetByIdAsync(id);
            return _mapper.Map<RoomResponseDTO?>(restroom);
        }

        public async Task<RoomResponseDTO> UpdateRoom(string restroomId, RoomRequestDTO request)
        {
            try
            {
                var existingRoom = await _roomRepository.GetByIdAsync(restroomId);
                if (existingRoom == null)
                {
                    throw new Exception("Room not found.");
                }

                var area = await _areaRepository.GetByIdAsync(request.AreaId);
                if (area == null)
                {
                    throw new Exception("Area does not exist.");
                }

                _mapper.Map(request, existingRoom);
                await _roomRepository.UpdateAsync(existingRoom);

                return _mapper.Map<RoomResponseDTO>(existingRoom);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to update restroom: {e.Message}");
            }
        }

    }
}
