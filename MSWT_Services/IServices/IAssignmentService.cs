using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IAssignmentService
    {
        Task<AssignmentResponseDTO> CreateAssignmentAsync(AssignmentRequestDTO request);
        Task DeleteAssigment(string id);
        Task<IEnumerable<AssignmentResponseDTO>> GetAllAssigments();
        Task<AssignmentResponseDTO> GetAssignmentById(string id);
        Task UpdateAssigment(Assignment assignment);
    }
}
