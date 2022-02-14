using System.ComponentModel.DataAnnotations;

namespace testidentityandjwt.BL.DTO
{
    public class Registerdto
    {
        public string Name { get; set; } = String.Empty; //cosi sto dando valore di default. altrimenti mettere stringa nullable

        public string Username { get; set; } = String.Empty;

        [EmailAddress(ErrorMessage = "not a valid email")]
        public string Email { get; set; } = String.Empty;

        public string Password { get; set; } = String.Empty;

        [Compare("Password", ErrorMessage = "password and confirmpassword do not match")]
        public string ConfirmPassword { get; set; } = String.Empty;

        public string Lastname { get; set; } = String.Empty;
        public DateTime birthday { get; set; }

    }

}
