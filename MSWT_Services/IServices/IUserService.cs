using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.ResponseDTO;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IUserService
    {
        #region CRUD User
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        Task AddUser(User userDto);
       
        Task DeleteUser(string id);
        #endregion

        Task<ResponseDTO> UpdateUserProfile(string userId, UserUpdateProfileDto dto);
        Task<ResponseDTO> UpdateUserStatusToQuit(string userId, string note);


        Task<ResponseDTO> Login(LoginRequestDTO userDto);
        Task<ResponseDTO> ChangePasswordAsync(string userId, ChangePasswordDto dto);

        //Task<string> GoogleLoginAsync(string idToken);
        Task RecoverUser(string id);
        //Task<ResponseDTO> GetUserProfile();
        //Task<ResponseDTO> UpdateUserProfile(UserUpdateDTO userDto);
        Task<IEnumerable<UserWithRoleDTO>> GetAllUserWithRoleAsync();

        Task<string> UpdateAvatarUrl(string id, IFormFile avatarFile);
        Task<IEnumerable<UserWithRoleDTO>> GetUnassignedWorkersAsync();
        Task<IEnumerable<UserWithRoleDTO>> GetUnassignedSupervisorsAsync();
    }
}
