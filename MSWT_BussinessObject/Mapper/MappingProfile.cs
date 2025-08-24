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
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : null));


            #endregion

            CreateMap<ScheduleDetailsRequestDTO, ScheduleDetail>();
            CreateMap<RoomRequestDTO, Room>();
            CreateMap<AreaRequestDTO, Area>();
            CreateMap<BuildingRequestDTO, Building>();
            CreateMap<AreaUpdateRequestDTO, Area>();
            CreateMap<ScheduleRequestDTO, Schedule>();
            CreateMap<ScheduleDetailsRequestDTO, ScheduleDetail>();
            CreateMap<ShiftRequestDTO, Shift>();
            CreateMap<AssignmentRequestDTO, Assignment>();
            CreateMap<AlertRequestDTO, Alert>();
            CreateMap<AlertResponseDTO, Alert>();
            CreateMap<Alert, AlertTrashBinDTO>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.TrashBin.Area.AreaName));

            CreateMap<ScheduleDetail, ScheduleDetailsResponseDTO>()
                .ForMember(dest => dest.Workers, opt => opt.MapFrom(src => src.WorkerGroup.WorkGroupMembers));
            CreateMap<Schedule, ScheduleResponseDTO>();
            CreateMap<Assignment, AssignmentResponseDTO>();
            CreateMap<Building, BuildingResponseDTO>();
            CreateMap<Room, RoomResponseDTO>();
            CreateMap<Room, RoomResponseDTO>()
             .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName));
            CreateMap<Area, AreaResponseDTO>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                .ForMember(dest => dest.BuildingName, opt => opt.MapFrom(src => src.Building.BuildingName));
            CreateMap<Shift, ShiftResponseDTO>();
            CreateMap<TrashBin, TrashBinResponseDTO>();
            CreateMap<ShiftSwapRequest, ShiftSwapResponseDTO>()
                .ForMember(dest => dest.TargetUserId, opt => opt.MapFrom(src => src.TargetUserId ?? string.Empty))
                .ForMember(dest => dest.TargetUserPhone, opt => opt.MapFrom(src => src.TargetUserPhone ?? string.Empty))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? string.Empty))
                .ForMember(dest => dest.Reason, opt => opt.MapFrom(src => src.Reason ?? string.Empty));
            CreateMap<ShiftSwapRequest, ShiftSwapResponseDTO>();
            CreateMap<WorkGroupMember, WorkGroupMemberResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<WorkerGroup, WorkerGroupResponse>();
            CreateMap<WorkGroupMemberRequestDTO, WorkGroupMember>();
            CreateMap<GroupAssignment, GroupAssignmentResponse>();

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

            CreateMap<WorkGroupMember, WorkGroupMemberResponse>();
            CreateMap<Request, RequestResponseDTO>()
                .ForMember(dest => dest.WorkerName, opt => opt.MapFrom(src => src.Worker.FullName));
            CreateMap<ScheduleDetail, ScheduleDetailsResponseDTO>()
                .ForMember(dest => dest.WorkerGroupName, opt => opt.MapFrom(src => src.WorkerGroup.WorkerGroupName))
                .ForMember(dest => dest.GroupAssignmentName, opt => opt.MapFrom(src => src.GroupAssignment.AssignmentGroupName))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.AreaName))
                ;
        }
    }
}
