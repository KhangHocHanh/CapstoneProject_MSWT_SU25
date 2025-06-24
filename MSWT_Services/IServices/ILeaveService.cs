using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface ILeaveService 
    {
        #region CRUD Category
        Task<IEnumerable<Leaf>> GetAllLeafs();
        Task<Leaf> GetLeafById(string id);
        Task AddLeaf(Leaf Leaf);
        Task UpdateLeaf(Leaf Leaf);
        Task DeleteLeaf(string id);
        #endregion
    }
}
