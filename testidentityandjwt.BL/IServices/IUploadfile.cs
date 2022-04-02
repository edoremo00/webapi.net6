using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.BL.IServices
{
    public interface IUploadfile
    {
        Task<string>UploadFile(IFormFile file);
        Task<string>UploadFile(IFormFile file, string foruserid);
        Task<bool> Deletefile(string filename);

        Task<object> Getallfilesincontainer();





    }
}
