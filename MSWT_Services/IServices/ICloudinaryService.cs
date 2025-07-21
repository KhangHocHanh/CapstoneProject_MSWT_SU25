using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
