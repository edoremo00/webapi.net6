using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testidentityandjwt.DAL.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }=String.Empty;

        [EmailAddress(ErrorMessage ="not a valid email")]
        public string Email { get; set; } = String.Empty;
        public DateTime Birthday{ get; set; }

        public string Userid { get; set; } = String.Empty;
    }
}
