using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.DAL.Entities
{
    public class MyUser:IdentityUser
    {
        public DateTime birthday { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
