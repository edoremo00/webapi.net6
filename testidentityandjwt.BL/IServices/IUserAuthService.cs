using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using testidentityandjwt.BL.DTO;
using testidentityandjwt.BL.Services;

namespace testidentityandjwt.BL.IServices
{
    public interface IUserAuthService
    {
        Task<bool> RegisterUser(Registerdto register);
        JwtSecurityToken createtoken(List<Claim> userclaim);
        Task<JwtSecurityToken?> Login(LoginDTO loginDTO);
        
        //public event UserregisteredEventHandler? UserRegistered;

       //public delegate object RegisterUserEventHandler(object source, UserArgs userargs);
    }
    
    
    //public delegate Task<object> UserregisteredEventHandler(object source, EventArgs args);
}


