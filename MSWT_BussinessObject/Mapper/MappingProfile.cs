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
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

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
                .ForMember(dest => dest.WorkerId, opt => opt.Ignore())
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
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now))
                .ForMember(dest => dest.ReportType, opt => opt.Ignore()) // gán thủ công
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToVietnamese()));

            // Cho leader
            CreateMap<ReportCreateDtoWithType, Report>()
                .IncludeBase<ReportCreateDto, Report>()
                .ForMember(dest => dest.ReportType, opt => opt.MapFrom(src => src.ReportType.ToVietnamese()));
            CreateMap<Report, ReportWithRoleDto>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName))
    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.User.Role.RoleName));

            CreateMap<Report, ReportWithUserNameDTO>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.UserName : null));


            #endregion


            CreateMap<FloorRequestDTO, Floor>();
            CreateMap<AreaRequestDTO, Area>();
            CreateMap<RestroomRequestDTO, Restroom>();
            CreateMap<AreaUpdateRequestDTO, Area>();
            CreateMap<ScheduleRequestDTO, Schedule>();
            CreateMap<ScheduleDetailsRequestDTO, ScheduleDetail>();
            CreateMap<ShiftRequestDTO, Shift>();
            CreateMap<AssignmentRequestDTO, Assignment>();
            CreateMap<AlertRequestDTO, Alert>();
            CreateMap<AlertResponseDTO, Alert>();

            CreateMap<Assignment, AssignmentResponseDTO>();
            CreateMap<Restroom, RestroomResponseDTO>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName));
            CreateMap<Floor, FloorResponseDTO>();
            CreateMap<Area, AreaResponseDTO>()
            .ForMember(dest => dest.FloorNumber, opt => opt.MapFrom(src => src.Floor.FloorNumber));
            CreateMap<Schedule, ScheduleResponseDTO>()
                .ForMember(dest => dest.RestroomNumber, opt => opt.MapFrom(src => src.Restroom.RestroomNumber))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName))

                .ForMember(dest => dest.Restroom, opt => opt.MapFrom(src => src.Restroom))
                .ForMember(dest => dest.TrashBin, opt => opt.MapFrom(src => src.TrashBin));
            CreateMap<ScheduleDetail, ScheduleDetailsResponseDTO>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Schedule.Area.AreaName))
                .ForMember(dest => dest.ScheduleName, opt => opt.MapFrom(src => src.Schedule.ScheduleName))
                .ForMember(dest => dest.AssignmentName, opt => opt.MapFrom(src => src.Assignment.AssigmentName))
                
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule));
            CreateMap<Shift, ShiftResponseDTO>();
            CreateMap<TrashBin, TrashBinResponseDTO>();

            #region User

            CreateMap<UserCreateDto, User>()
    .ForMember(dest => dest.UserId, opt => opt.Ignore())
    .ForMember(dest => dest.CreateAt, opt => opt.Ignore())
    .ForMember(dest => dest.Status, opt => opt.Ignore())
    .ForMember(dest => dest.Rating, opt => opt.Ignore())
    .ForMember(dest => dest.ReasonForLeave, opt => opt.Ignore());
            CreateMap<UserUpdateProfileDto, User>()
                .ForMember(dest => dest.Status, opt => opt.Ignore()) // không cho update Status
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // giữ nguyên ID
                .ForMember(dest => dest.RoleId, opt => opt.Ignore()) // giữ nguyên Role
                .ForMember(dest => dest.CreateAt, opt => opt.Ignore()) // giữ nguyên ngày tạo
                .ForMember(dest => dest.Password, opt => opt.Ignore()) // không cập nhật pass
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.ReasonForLeave, opt => opt.Ignore());

            CreateMap<User, UserWithRoleDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Role.Description));
            #endregion

            #region Trashbin    
            CreateMap<TrashBin, TrashbinWithAreaNameDTO>()
    .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area != null ? src.Area.AreaName : null));
            #endregion
        }
    }
}
