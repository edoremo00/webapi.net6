using System.ComponentModel.DataAnnotations;

namespace testidentityandjwt.BL.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } = String.Empty;

        [EmailAddress(ErrorMessage = "not a valid email")]
        public string Email { get; set; } = String.Empty;
        public DateTime Birthday { get; set; }

        public string Userid { get; set; } = String.Empty;
    }
}
