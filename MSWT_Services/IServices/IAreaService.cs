using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IAreaService
    {
        Task AddArea(Area area);
        Task DeleteArea(string id);
        Task<IEnumerable<Area>> GetAllAreas();
        Task<Area> GetAreaById(string id);
        Task UpdateArea(Area area);
    }
}
