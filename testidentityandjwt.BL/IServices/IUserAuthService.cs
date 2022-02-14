using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using testidentityandjwt.BL.DTO;

namespace testidentityandjwt.BL.IServices
{
    public interface IUserAuthService
    {
        Task<bool> RegisterUser(Registerdto register);
        JwtSecurityToken createtoken(List<Claim> userclaim);
        Task<JwtSecurityToken?> Login(LoginDTO loginDTO);
    }
}
