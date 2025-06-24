using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using AutoMapper;
using MSWT_BussinessObject.Enum;
using static MSWT_BussinessObject.Enum.Enum;
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

            #region Request

            // Map từ DTO → Entity
            CreateMap<RequestCreateDto, Request>()
                .ForMember(dest => dest.RequestId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.RequestDate, opt => opt.Ignore())
                .ForMember(dest => dest.ResolveDate, opt => opt.Ignore());
            #endregion

            #region Leave
            CreateMap<LeaveCreateDto, Leaf>()
                .ForMember(dest => dest.LeaveId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkerId, opt => opt.Ignore())
                .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.ToVietnamese()))
                .ForMember(dest => dest.TotalDays, opt => opt.Ignore())
                .ForMember(dest => dest.RequestDate, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => ApprovalStatusEnum.ChuaDuyet.ToVietnamese()))
                .ForMember(dest => dest.ApprovedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ApprovalDate, opt => opt.Ignore())
                .ForMember(dest => dest.Note, opt => opt.Ignore());
            #endregion

            #region Report
            // Cho nhân viên (không map ReportType vì sẽ gán mặc định)
            CreateMap<ReportCreateDto, Report>()
                .ForMember(dest => dest.ReportId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ReportStatus.DaGui.ToVietnamese()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.Now)))
                .ForMember(dest => dest.ReportType, opt => opt.Ignore()) // gán thủ công
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToVietnamese()));

            // Cho leader
            CreateMap<ReportCreateDtoWithType, Report>()
                .IncludeBase<ReportCreateDto, Report>()
                .ForMember(dest => dest.ReportType, opt => opt.MapFrom(src => src.ReportType.ToVietnamese()));
            #endregion
            CreateMap<AreaRequestDTO, Area>();

            CreateMap<Restroom, RestroomResponseDTO>();
            CreateMap<Floor, FloorResponseDTO>();
            CreateMap<Area, AreaResponseDTO>();
            CreateMap<Schedule, ScheduleResponseDTO>();
        }
    }
}
