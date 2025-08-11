using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.Model
{
    public class Holiday
    {
        public string? HolidayId { get; set; } = null!;

        public DateTime Date { get; set; }

        public string? HolidayName { get; set; }
    }
}
