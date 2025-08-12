using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using MSWT_BussinessObject;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTService _jWTService;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinary;
        private readonly IScheduleDetailsRepository _scheduleDetailsRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor, IJWTService jWTService, AutoMapper.IMapper mapper, IUnitOfWork unitOfWork, ICloudinaryService cloudinary, IScheduleDetailsRepository scheduleDetailsRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
            _jWTService = jWTService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cloudinary = cloudinary;
            _scheduleDetailsRepository = scheduleDetailsRepository;
        }

        #region CRUD User
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var adminId = await _roleRepository.GetIdByNameAsync("admin");
            var staffId = await _roleRepository.GetIdByNameAsync("staff");

            if (user == null)
                return null;
            // Get the current user's role
            var currentUserRole = _httpContextAccessor.HttpContext.User.Claims
                                .FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Prevent staff from viewing admin users and other staffs
            if (currentUserRole.ToLower() == "staff" && (user.RoleId == adminId.RoleId || user.RoleId == staffId.RoleId))
            {
                return null;
            }
            return user;
        }
        public async Task DeleteUser(string id)
        {
            await _userRepository.SoftDeleteAsync(id);
        }
        #endregion
        public async Task RecoverUser(string id)
        {
            await _userRepository.RecoverAsync(id);
        }
        // Hàm Login
        public async Task<ResponseDTO> Login(LoginRequestDTO userDto)
        {
            try
            {
                var account = await _userRepository.GetByUsernameAsync(userDto.Username);

                // Nếu không tìm thấy, thử tìm theo số điện thoại
                if (account == null)
                {
                    account = await _userRepository.GetByPhoneAsync(userDto.Username);
                }

                if (account == null)
                {
                    return new ResponseDTO(Const.FAIL_READ_CODE, "Sai tên đăng nhập/số điện thoại hoặc mật khẩu");
                }

                if (account.Password != userDto.Password)  // Nếu dùng hash, cần giải mã password
                {
                    return new ResponseDTO(Const.FAIL_READ_CODE, "Sai tên đăng nhập/số điện thoại hoặc mật khẩu");
                }

                var jwt = _jWTService.GenerateToken(account);

                var claims = new JwtSecurityTokenHandler().ReadJwtToken(jwt).Claims;
                foreach (var claim in claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                }


                return new ResponseDTO(Const.SUCCESS_READ_CODE, "Login successful", jwt);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception: {ex.Message}");
                return new ResponseDTO(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task AddUser(User userDto)
        {
            await _userRepository.AddAsync(userDto);
        }
        public async Task<ResponseDTO> UpdateUserProfile(string userId, UserUpdateProfileDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return new ResponseDTO(Const.FAIL_READ_CODE, "Không tìm thấy người dùng.");

            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Cập nhật thông tin cá nhân thành công.");
        }

        public async Task<ResponseDTO> UpdateUserStatusToQuit(string userId, string note)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return new ResponseDTO(Const.FAIL_READ_CODE, "Không tìm thấy người dùng.");

            var statusEnum = UserStatusHelper.ToEnum(user.Status);
            if (statusEnum != UserStatusEnum.HoatDong)
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Chỉ có thể chuyển trạng thái từ 'Hoạt động' sang 'Thôi việc'.");

            user.Status = UserStatusHelper.ToStringStatus(UserStatusEnum.ThoiViec);
            user.ReasonForLeave = note;
            await _userRepository.UpdateAsync(user);

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Đã cập nhật trạng thái sang 'Thôi việc'.");
        }
        public async Task<IEnumerable<UserWithRoleDTO>> GetAllUserWithRoleAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserWithRoleDTO>>(users);

        }
        public async Task<ResponseDTO> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return new ResponseDTO(Const.FAIL_READ_CODE, "Không tìm thấy người dùng.");

            // Kiểm tra mật khẩu cũ
            if (user.Password != dto.OldPassword) // Nếu có hash: !Verify(dto.OldPassword, user.Password)
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Mật khẩu cũ không chính xác.");

            // Kiểm tra mật khẩu mới khớp nhau
            if (dto.NewPassword != dto.ConfirmNewPassword)
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Mật khẩu mới không khớp nhau.");
            // Kiểm tra điều kiện mật khẩu mới
            if (!IsValidPassword(dto.NewPassword))
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Mật khẩu phải bắt đầu bằng chữ hoa, có số và tối thiểu 7 ký tự.");


            if (UserStatusHelper.ToEnum(user.Status) == UserStatusEnum.ChuaXacThuc)
            {
                user.Status = UserStatusHelper.ToStringStatus(UserStatusEnum.HoatDong);
            }


            // Cập nhật mật khẩu
            user.Password = dto.NewPassword; 
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Đổi mật khẩu thành công.");
        }

        public async Task<string> UpdateAvatarUrl(string id, IFormFile avatarFile)
        {
            try
            {
                if (avatarFile == null || avatarFile.Length == 0)
                    throw new Exception("No file provided.");

                var uploadResult = await _cloudinary.UploadFile(avatarFile);
                if (uploadResult == null || string.IsNullOrEmpty(uploadResult))
                    throw new Exception("Cloudinary upload failed.");

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    throw new Exception("User not found.");

                user.Image = uploadResult;
                await _userRepository.UpdateAsync(user);

                return user.Image;
            }
            catch (Exception ex)
            {
                // Log exception if needed
                throw new Exception($"Error updating image: {ex.Message}", ex);
            }
        }
        private bool IsValidPassword(string password)
        {
            if (password.Length < 7) return false;
            if (!char.IsUpper(password[0])) return false;
            if (!password.Any(char.IsDigit)) return false;
            return true;
        }


        public Task<User> GetUserByPhoneAsync(string phoneNumber)
        {
            return _userRepository.GetByPhoneAsync(phoneNumber);
        }

        public async Task<IEnumerable<UserWithRoleDTO>> GetUnassignedWorkersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var filteredUsers = users
                .Where(u => u.RoleId == "RL04" && u.IsAssigned == "No");

            return _mapper.Map<IEnumerable<UserWithRoleDTO>>(filteredUsers);
        }

        public async Task<IEnumerable<UserWithRoleDTO>> GetUnassignedSupervisorsAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var filteredUsers = users
                .Where(u => u.RoleId == "RL03" && u.IsAssigned == "No");

            return _mapper.Map<IEnumerable<UserWithRoleDTO>>(filteredUsers);

        }

        public Task UpdatePasswordAsync(string userId, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO> ChangePasswordByPhoneNumberAsync( ChangePasswordByPhoneNumberDto dto)
        {
            var user = await _userRepository.GetByPhoneAsync(dto.PhoneNumber);
            if (user == null)
                return new ResponseDTO(Const.FAIL_READ_CODE, "Không tìm thấy người dùng.");

            // Kiểm tra mật khẩu cũ
            if (user.Phone != dto.PhoneNumber) 
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Số điện thoại chưa đăng ký người dùng.");

            // Kiểm tra mật khẩu mới khớp nhau
            if (dto.NewPassword != dto.ConfirmNewPassword)
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Mật khẩu mới không khớp nhau.");
            // Kiểm tra điều kiện mật khẩu mới
            if (!IsValidPassword(dto.NewPassword))
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Mật khẩu phải bắt đầu bằng chữ hoa, có số và tối thiểu 7 ký tự.");


            // Cập nhật mật khẩu
            user.Password = dto.NewPassword;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Đổi mật khẩu thành công.");
        }
    }
}
