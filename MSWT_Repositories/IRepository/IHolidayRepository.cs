using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.IRepository
{
    public interface IHolidayRepository
    {
        Task<List<DateTime>> GetAllHolidaysAsync();
    }
}
