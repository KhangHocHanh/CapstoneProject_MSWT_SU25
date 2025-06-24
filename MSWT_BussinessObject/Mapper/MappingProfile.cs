using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;

namespace MSWT_BussinessObject.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add your mappings here
            // For example:
            // CreateMap<SourceModel, DestinationModel>();
            // CreateMap<DestinationModel, SourceModel>();
            CreateMap<AreaRequestDTO, Area>();

            CreateMap<Restroom, RestroomResponseDTO>();
            CreateMap<Floor, FloorResponseDTO>();
            CreateMap<Area, AreaResponseDTO>();
            CreateMap<Schedule, ScheduleResponseDTO>();
        }
    }
}
