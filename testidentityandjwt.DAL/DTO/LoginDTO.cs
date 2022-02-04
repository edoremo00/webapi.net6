using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.DAL.DTO
{
    public class LoginDTO
    {
        public string email { get; set; }=String.Empty;
        public string Password { get; set; }=String.Empty ;
    }
}
