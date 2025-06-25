using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MSWT_BussinessObject;
using MSWT_BussinessObject.Enum;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.Enum.Enum;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTService _jWTService;
        private readonly AutoMapper.IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor, IJWTService jWTService, AutoMapper.IMapper mapper)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _httpContextAccessor = httpContextAccessor;
            _jWTService = jWTService;
            _mapper = mapper;
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
            if (statusEnum != UserStatusEnum.Trong)
                return new ResponseDTO(Const.FAIL_UPDATE_CODE, "Chỉ có thể chuyển trạng thái từ 'Trống lịch' sang 'Thôi việc'.");

            user.Status = UserStatusHelper.ToStringStatus(UserStatusEnum.ThoiViec);
            user.ReasonForLeave = note;
            await _userRepository.UpdateAsync(user);

            return new ResponseDTO(Const.SUCCESS_UPDATE_CODE, "Đã cập nhật trạng thái sang 'Thôi việc'.");
        }
    }
}
